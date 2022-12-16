using Microsoft.EntityFrameworkCore;
using UniQuanda.Core.Application.Repositories;
using UniQuanda.Core.Domain.Entities.App;
using UniQuanda.Core.Domain.Enums;
using UniQuanda.Infrastructure.Presistence.AppDb;
using UniQuanda.Infrastructure.Presistence.AppDb.Models;
using UniQuanda.Infrastructure.Presistence.ExtensionsEF;

namespace UniQuanda.Infrastructure.Repositories
{
    public class QuestionRepository : IQuestionRepository
    {
        private readonly AppDbContext _context;
        public QuestionRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<int> AddQuestionAsync(
            int contentId, int userId,
            IEnumerable<(int order, int tagId)> tags,
            string title, string rawText, string text,
            IEnumerable<string> imageNames,
            DateTime creationTime,
            CancellationToken ct)
        {
            //create transaction 
            await using var ctx = await _context.Database.BeginTransactionAsync(ct);
            try
            {
                //create content
                var content = new Content
                {
                    Id = contentId,
                    RawText = rawText,
                    Text = text,
                    ContentType = ContentTypeEnum.Question
                };
                await _context.Contents.AddAsync(content, ct);

                //create images and connect to content
                foreach (var imageName in imageNames)
                {
                    var image = new Image
                    {
                        URL = imageName
                    };
                    await _context.Images.AddAsync(image, ct);
                    var imageInContent = new ImageInContent
                    {
                        ContentIdNavigation = content,
                        ImageIdNavigation = image,
                    };
                    await _context.ImagesInContent.AddAsync(imageInContent, ct);
                }
                // create question
                var question = new Question
                {
                    ContentId = contentId,
                    CreatedAt = creationTime,
                    Header = title,

                };
                await _context.Questions.AddAsync(question, ct);

                //create tags in question
                foreach (var tag in tags)
                {
                    var tagInQuestion = new TagInQuestion
                    {
                        TagId = tag.tagId,
                        Order = tag.order,
                        QuestionIdNavigation = question
                    };
                    await _context.TagsInQuestions.AddAsync(tagInQuestion, ct);
                }
                //add creator interaction with question
                var creatorInteraction = new AppUserQuestionInteraction
                {
                    AppUserId = userId,
                    IsCreator = true,
                    IsFollowing = true,
                    IsViewed = true,
                    QuestionIdNavigation = question
                };

                await _context.AppUsersQuestionsInteractions.AddAsync(creatorInteraction, ct);

                var totalCount = tags.Count() + imageNames.Count() * 2 + 3;
                var rowsAdded = await _context.SaveChangesAsync(ct);
                if (rowsAdded != totalCount)
                {
                    await ctx.RollbackAsync(ct);
                    return 0;
                }
                await ctx.CommitAsync(ct);
                return question.Id;
            }
            catch
            {
                await ctx.RollbackAsync(ct);
                return 0;
            }
        }

        public async Task<IEnumerable<QuestionEntity>> GetQuestionsAsync(
            int take, 
            int skip, 
            IEnumerable<int>? tags, 
            OrderDirectionEnum orderBy, 
            QuestionSortingEnum sortBy, 
            string? searchText,
            CancellationToken ct)
        {
            IQueryable<Question> questions = _context.Questions;

            if (searchText is not null) {
                questions = questions.Where(q => EF.Functions.ILike(q.Header, $"%{searchText}%"));
            }

            if (tags != null)
            {
                foreach (var tag in tags)
                {
                    questions = questions
                        .Where(q => q.TagsInQuestion
                            .Select(tq => tq.TagId)
                            .Contains(tag) ||
                            q.TagsInQuestion
                            .Select(tq => tq.TagIdNavigation.ParentTagId)
                            .Contains(tag)
                        );
                }
            }
            switch (sortBy)
            {
                case QuestionSortingEnum.PublicationDate:
                    questions = questions.OrderBy(q => q.CreatedAt, orderBy);
                    break;
                case QuestionSortingEnum.Views:
                    questions = questions.OrderBy(q => q.ViewsCount, orderBy);
                    break;
                case QuestionSortingEnum.Answers:
                    questions = questions.OrderBy(q => q.Answers.Count, orderBy);
                    break;

            }
            var que = await questions
                .Select(q => new QuestionEntity
                {
                    Id = q.Id,
                    Header = q.Header,
                    Content = new Core.Domain.ValueObjects.Content()
                    {
                        RawText = q.ContentIdNavigation.RawText
                    },
                    CreatedAt = q.CreatedAt,
                    ViewsCount = q.ViewsCount,
                    AnswersCount = q.Answers.Count(),
                    Tags = q.TagsInQuestion.Select(t => new TagEntity
                    {
                        Name = t.TagIdNavigation.Name
                    }),
                    User = new AppUserEntity()
                    {
                        Id = q.AppUsersQuestionInteractions
                            .Where(a => a.IsCreator)
                            .Select(a => a.AppUserId)
                            .FirstOrDefault(),
                        Nickname = q.AppUsersQuestionInteractions
                            .Where(a => a.IsCreator)
                            .Select(a => a.AppUserIdNavigation.Nickname)
                            .FirstOrDefault(),
                        Avatar = q.AppUsersQuestionInteractions
                                .Where(a => a.IsCreator)
                                .Select(a => a.AppUserIdNavigation.Avatar)
                                .FirstOrDefault(),
                    },
                    HasCorrectAnswer = q.Answers.Any(a => a.IsCorrect)
                }).Skip(skip).Take(take).ToListAsync(ct);

            
            return que;
        }

        public async Task<int> GetQuestionsCountAsync(IEnumerable<int>? tags, string? searchText, CancellationToken ct)
        {
            IQueryable<Question> questions = _context.Questions;

            if (searchText is not null) {
                questions = questions.Where(q => EF.Functions.ILike(q.Header, $"%{searchText}%"));
            }

            if (tags != null)
            {
                foreach (var tag in tags)
                {
                    questions = questions
                        .Where(q => q.TagsInQuestion
                            .Select(tq => tq.TagId)
                            .Contains(tag) || 
                            q.TagsInQuestion
                            .Select(tq => tq.TagIdNavigation.ParentTagId)
                            .Contains(tag)
                        );
                }
            }
            return await questions.CountAsync(ct);
        }

        public async Task<IEnumerable<QuestionEntity>> GetQuestionsOfUniversityAsync(int universityId, int take, int skip, CancellationToken ct)
        {
            return await _context.Questions
                .Where(q => q.AppUsersQuestionInteractions
                    .Where(a => a.IsCreator)
                    .Select(a => 
                        a.AppUserIdNavigation.AppUserInUniversities
                        .Where(uu =>
                            uu.UniversityId == universityId
                        ).Any()
                    )
                    .FirstOrDefault())
                .OrderByDescending(q => q.CreatedAt)
                .Select(q => new QuestionEntity() {
                    Id = q.Id,
                    Header = q.Header,
                    Content = new Core.Domain.ValueObjects.Content()
                    {
                        RawText = q.ContentIdNavigation.RawText
                    },
                    CreatedAt = q.CreatedAt,
                    ViewsCount = q.ViewsCount,
                    AnswersCount = q.Answers.Count(),
                    Tags = q.TagsInQuestion.Select(t => new TagEntity
                    {
                        Name = t.TagIdNavigation.Name
                    }),
                    HasCorrectAnswer = q.Answers.Any(a => a.IsCorrect),
                    User = new AppUserEntity()
                    {
                        Id = q.AppUsersQuestionInteractions
                            .Where(a => a.IsCreator)
                            .Select(a => a.AppUserId)
                            .FirstOrDefault(),
                        Nickname = q.AppUsersQuestionInteractions
                            .Where(a => a.IsCreator)
                            .Select(a => a.AppUserIdNavigation.Nickname)
                            .FirstOrDefault(),
                        Avatar = q.AppUsersQuestionInteractions
                                .Where(a => a.IsCreator)
                                .Select(a => a.AppUserIdNavigation.Avatar)
                                .FirstOrDefault(),
                    }
                })
                .Skip(skip)
                .Take(take)
                .ToListAsync(ct);
        }

        public async Task<int> GetQuestionsOfUniversityCountAsync(int universityId, CancellationToken ct)
        {
            return await _context.Questions
                .Where(q => q.AppUsersQuestionInteractions
                    .Where(a => a.IsCreator)
                    .Select(a =>
                        a.AppUserIdNavigation.AppUserInUniversities
                        .Where(uu =>
                            uu.UniversityId == universityId
                        ).Any()
                    )
                    .FirstOrDefault())
                .CountAsync(ct);
        }

        public async Task<IEnumerable<QuestionEntity>> GetQuestionsOfUserAsync(int userId, int take, int skip, CancellationToken ct)
        {
            return await _context.AppUsersQuestionsInteractions
                .Where(a => a.AppUserId == userId && a.IsCreator)
                .Select(aqi => new QuestionEntity
                {
                    Id = aqi.QuestionId,
                    Header = aqi.QuestionIdNavigation.Header,
                    Content = new Core.Domain.ValueObjects.Content()
                    {
                        RawText = aqi.QuestionIdNavigation.ContentIdNavigation.RawText
                    },
                    CreatedAt = aqi.QuestionIdNavigation.CreatedAt,
                    ViewsCount = aqi.QuestionIdNavigation.ViewsCount,
                    AnswersCount = aqi.QuestionIdNavigation.Answers.Count(),
                    Tags = aqi.QuestionIdNavigation.TagsInQuestion.Select(t => new TagEntity
                    {
                        Name = t.TagIdNavigation.Name
                    }),
                    HasCorrectAnswer = aqi.QuestionIdNavigation.Answers.Any(a => a.IsCorrect)
                }).Skip(skip).Take(take).ToListAsync(ct);
        }

        public Task<int> GetQuestionsOfUserCountAsync(int userId, CancellationToken ct)
        {
            return _context.AppUsersQuestionsInteractions
                .Where(a => a.AppUserId == userId && a.IsCreator)
                .CountAsync(ct);
        }
    }
}
