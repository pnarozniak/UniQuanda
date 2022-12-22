using Microsoft.EntityFrameworkCore;
using UniQuanda.Core.Application.Repositories;
using UniQuanda.Core.Domain.Entities.App;
using UniQuanda.Core.Domain.ValueObjects;
using UniQuanda.Infrastructure.Presistence.AppDb;
using UniQuanda.Infrastructure.Presistence.AppDb.Models;

namespace UniQuanda.Infrastructure.Repositories
{
    public class TestRepository : ITestRepository
    {
        private static readonly int UserTestsLimit = 10;
        private static readonly int TestQuestionsLimit = 10;

        private readonly AppDbContext _context;
        public TestRepository(AppDbContext context)
        {
            _context = context;
        }

		public async Task<int?> GenerateTestAsync(int idUser, IEnumerable<int> tagsIds, CancellationToken ct)
		{
            var tags = await _context.Tags.Where(t => tagsIds.Contains(t.Id)).ToListAsync(ct);
            if (tags is null || tags.Count() == 0) return null;

			var questions = await _context.Questions
                .Where(q => q.Answers.Any(a => a.IsCorrect))
                .Where(q => q.TagsInQuestion.Any(tq => 
                    tagsIds.Contains(tq.TagId)
                    || (tq.TagIdNavigation.ParentTagId != null && tagsIds.Contains(tq.TagIdNavigation.ParentTagId.Value)))
                )
                .OrderBy(tq => Guid.NewGuid())
                .Take(TestQuestionsLimit)
                .ToListAsync(ct);

            if (questions is null || questions.Count == 0) return null;

            var test = new Test
            {
                CreatedAt = DateTime.Now,
                IdCreator = idUser,
                TestTags = tags.Select(t => new TestTag { IdTag = t.Id }).ToList(),
                TestQuestions = questions.Select(q => new TestQuestion
                {
                    IdQuestionNavigation = q
                }).ToList()
            };
            await _context.Tests.AddAsync(test, ct);

            var userTests = await _context.Tests
                .OrderBy(t => t.CreatedAt)
                .Where(t => t.IdCreator == idUser)
                .ToListAsync(ct);

            if (userTests.Count() >= UserTestsLimit) {
                var oldestTest = userTests.First()!;
                _context.Tests.Remove(oldestTest);
            }

            var success = await _context.SaveChangesAsync(ct) > 0;
            return !success ? null : test.Id;
		}

		public async Task<TestEntity?> GetTestAsync(int idTest, CancellationToken ct)
		{
			return await _context.Tests
                .Where(t => t.Id == idTest)
                .Select(t => new TestEntity {
                    Id = t.Id,
                    CreatedAt = t.CreatedAt,
                    IsFinished = t.IsFinished,
                    IdCreator = t.IdCreator,
                    Tags = t.TestTags.Select(t => new TestTagValueObject
                    {
                        Id = t.IdTag,
                        Name = t.IdTagNavigation.Name
                    }),
                    Questions = t.TestQuestions.Select(tq => new TestQuestionValueObject
                        {
                            Id = tq.IdQuestionNavigation.Id,
                            CreatedAt = tq.IdQuestionNavigation.CreatedAt,
                            Header = tq.IdQuestionNavigation.Header,
                            HTML = tq.IdQuestionNavigation.ContentIdNavigation.RawText,
                            Answer = tq.IdQuestionNavigation.Answers
                                .Where(a => a.IsCorrect)
                                .Select(a => new TestAnswerValueObject {
                                    Id = a.Id,
                                    HTML = a.ContentIdNavigation.RawText,
                                    CreatedAt = a.CreatedAt,
                                    CommentsCount = a.Comments.Count()
                                })
                                .SingleOrDefault()!
                        })
                    })
                    .SingleOrDefaultAsync(ct);
		}

		public async Task<IEnumerable<TestEntity>> GetUserTestsAsync(int idUser, CancellationToken ct)
		{
			return await _context.Tests
                .Where(t => t.IdCreator == idUser)
                .Select(t => new TestEntity {
                    Id = t.Id,
                    CreatedAt = t.CreatedAt,
                    IsFinished = t.IsFinished,
                    IdCreator = t.IdCreator,
                    Tags = t.TestTags.Select(t => new TestTagValueObject
                    {
                        Id = t.IdTag,
                        Name = t.IdTagNavigation.Name
                    }),
                }).ToListAsync(ct);
		}

		public async Task<bool> MarkTestAsFinishedAsync(int idUser, int idTest, CancellationToken ct)
		{
            var test = await _context.Tests
                .Where(t => t.Id == idTest && t.IdCreator == idUser && t.IsFinished == false)
                .SingleOrDefaultAsync(ct);
            if (test is null) return false;

			test.IsFinished = true;
            return await _context.SaveChangesAsync() == 1;
		}
	}
}