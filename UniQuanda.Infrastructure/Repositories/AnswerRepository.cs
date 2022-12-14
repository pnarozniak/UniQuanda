using Microsoft.EntityFrameworkCore;
using UniQuanda.Core.Application.Repositories;
using UniQuanda.Core.Domain.Entities.App;
using UniQuanda.Infrastructure.Presistence.AppDb;

namespace UniQuanda.Infrastructure.Repositories
{
    public class AnswerRepository : IAnswerRepository
    {
        private readonly AppDbContext _context;
        public AnswerRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<AnswerEntity>> GetAnswersOfUserAsync(int userId, int take, int skip, CancellationToken ct)
        {
            return await _context.AppUsersAnswersInteractions.Where(
                aai => aai.IsCreator && aai.AppUserId == userId
                && aai.AnswerIdNavigation.IsDeleted == false
                )
                .Select(aai => new AnswerEntity()
                {
                    Id = aai.AnswerId,
                    ParentAnswerId = aai.AnswerIdNavigation.ParentAnswerId,
                    Content = new Core.Domain.ValueObjects.Content()
                    {
                        RawText = aai.AnswerIdNavigation.ContentIdNavigation.RawText,
                    },
                    Question = new QuestionEntity()
                    {
                        Id = aai.AnswerIdNavigation.ParentQuestionId,
                        Header = aai.AnswerIdNavigation.ParentQuestionIdNavigation.Header,
                        Tags = aai.AnswerIdNavigation.ParentQuestionIdNavigation.TagsInQuestion.Select(tiq => new TagEntity() { 
                            Name = tiq.TagIdNavigation.Name,
                        }),
                    },
                    CreatedAt = aai.AnswerIdNavigation.CreatedAt,
                    Likes = aai.AnswerIdNavigation.LikeCount,
                    IsCorrect = aai.AnswerIdNavigation.IsCorrect,
                })
                .Skip(skip)
                .Take(take)
                .ToListAsync(ct);
        }

        public async Task<int> GetAnswersOfUserCountAsync(int userId, CancellationToken ct)
        {
            return await _context.AppUsersAnswersInteractions.Where(aai => aai.IsCreator && aai.AppUserId == userId).CountAsync(ct);
        }
    }
}
