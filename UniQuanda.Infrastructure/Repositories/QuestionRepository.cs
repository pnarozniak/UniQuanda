using Microsoft.EntityFrameworkCore;
using UniQuanda.Core.Application.Repositories;
using UniQuanda.Core.Domain.Entities.App;
using UniQuanda.Core.Domain.Enums;
using UniQuanda.Core.Domain.Enums.Results;
using UniQuanda.Core.Domain.ValueObjects;
using UniQuanda.Infrastructure.Presistence.AppDb;
using UniQuanda.Infrastructure.Presistence.AppDb.Models;
using UniQuanda.Infrastructure.Presistence.ExtensionsEF;
using Content = UniQuanda.Infrastructure.Presistence.AppDb.Models.Content;
using Image = UniQuanda.Infrastructure.Presistence.AppDb.Models.Image;

namespace UniQuanda.Infrastructure.Repositories;

public class QuestionRepository : IQuestionRepository
{
    private readonly AppDbContext _appContext;
    public QuestionRepository(AppDbContext appContext)
    {
        _appContext = appContext;
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
        await using var ctx = await _appContext.Database.BeginTransactionAsync(ct);
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
            await _appContext.Contents.AddAsync(content, ct);

            //create images and connect to content
            foreach (var imageName in imageNames)
            {
                var image = new Image
                {
                    URL = imageName
                };
                await _appContext.Images.AddAsync(image, ct);
                var imageInContent = new ImageInContent
                {
                    ContentIdNavigation = content,
                    ImageIdNavigation = image,
                };
                await _appContext.ImagesInContent.AddAsync(imageInContent, ct);
            }
            // create question
            var question = new Question
            {
                ContentId = contentId,
                CreatedAt = creationTime,
                Header = title,

            };
            await _appContext.Questions.AddAsync(question, ct);

            //create tags in question
            foreach (var tag in tags)
            {
                var tagInQuestion = new TagInQuestion
                {
                    TagId = tag.tagId,
                    Order = tag.order,
                    QuestionIdNavigation = question
                };
                await _appContext.TagsInQuestions.AddAsync(tagInQuestion, ct);
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

            await _appContext.AppUsersQuestionsInteractions.AddAsync(creatorInteraction, ct);

            var totalCount = tags.Count() + imageNames.Count() * 2 + 3;
            var rowsAdded = await _appContext.SaveChangesAsync(ct);
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
        IQueryable<Question> questions = _appContext.Questions;

        if (searchText is not null)
        {
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
                AnswersCount = q.Answers.Where(a => a.ParentAnswerId == null).Count(),
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
        IQueryable<Question> questions = _appContext.Questions;

        if (searchText is not null)
        {
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
        return await _appContext.Questions
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
            .Select(q => new QuestionEntity()
            {
                Id = q.Id,
                Header = q.Header,
                Content = new Core.Domain.ValueObjects.Content()
                {
                    RawText = q.ContentIdNavigation.RawText
                },
                CreatedAt = q.CreatedAt,
                ViewsCount = q.ViewsCount,
                AnswersCount = q.Answers.Where(a => a.ParentAnswerId == null).Count(),
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
        return await _appContext.Questions
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
        return await _appContext.AppUsersQuestionsInteractions
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
        return _appContext.AppUsersQuestionsInteractions
            .Where(a => a.AppUserId == userId && a.IsCreator)
            .CountAsync(ct);
    }


    public async Task<QuestionDetailsEntity?> GetQuestionDetailsAsync(int idQuestion, int? idLoggedUser, CancellationToken ct)
    {
        return await _appContext.Questions.Where(q => q.Id == idQuestion).Select(q => new QuestionDetailsEntity
        {
            Id = q.Id,
            Header = q.Header,
            Content = q.ContentIdNavigation.RawText,
            PublishDate = q.CreatedAt,
            AmountOfAnswers = q.Answers.Where(a => a.ParentAnswerId == null).Count(),
            Views = q.ViewsCount,
            Author = q.AppUsersQuestionInteractions.Where(qi => qi.IsCreator).Select(u => new AuthorContent
            {
                Id = u.AppUserIdNavigation.Id,
                Nickname = u.AppUserIdNavigation.Nickname,
                AvatarUrl = u.AppUserIdNavigation.Avatar,
                AcademicTitles = u.AppUserIdNavigation.AppUserTitles.Select(t => new AcademicTitleEntity
                {
                    Name = t.AcademicTitleIdNavigation.Name,
                    Order = t.Order,
                    AcademicTitleType = t.AcademicTitleIdNavigation.AcademicTitleType
                })
            }).SingleOrDefault()!,
            Tags = q.TagsInQuestion.Select(t => new QuestionDetailsTag
            {
                Id = t.TagId,
                Name = t.TagIdNavigation.Name,
                Order = t.Order
            }),
            HasCorrectAnswer = q.Answers.Any(a => a.IsCorrect)
        }).SingleOrDefaultAsync(ct);
    }

    public async Task<bool> CreateOrUpdateQuestionViewFromAppUserAsync(int idQuestion, int idLoggedUser, CancellationToken ct)
    {
        var questionInteraction = await _appContext.AppUsersQuestionsInteractions.SingleOrDefaultAsync(qi => qi.AppUserId == idLoggedUser && qi.QuestionId == idQuestion, ct);
        if (questionInteraction is null)
        {
            var newAppUserQuestionInteraction = new AppUserQuestionInteraction
            {
                AppUserId = idLoggedUser,
                QuestionId = idQuestion,
                IsViewed = true,
            };
            await _appContext.AppUsersQuestionsInteractions.AddAsync(newAppUserQuestionInteraction, ct);
            return await _appContext.SaveChangesAsync(ct) == 1;
        }

        questionInteraction.IsViewed = true;
        return await _appContext.SaveChangesAsync(ct) == 1;
    }

    public async Task<bool> UpdateQuestionFollowStatusAsync(int idQuestion, int idLoggedUser, CancellationToken ct)
    {
        var isFollowInteraction = await _appContext.AppUsersQuestionsInteractions.SingleOrDefaultAsync(ct => ct.QuestionId == idQuestion && ct.AppUserId == idLoggedUser, ct);
        if (isFollowInteraction is null)
        {
            var appUserQuestionInteraction = new AppUserQuestionInteraction
            {
                QuestionId = idQuestion,
                AppUserId = idLoggedUser,
                IsFollowing = true
            };
            await _appContext.AppUsersQuestionsInteractions.AddAsync(appUserQuestionInteraction, ct);
            return await _appContext.SaveChangesAsync(ct) == 1;
        }

        isFollowInteraction.IsFollowing = !isFollowInteraction.IsFollowing;
        return await _appContext.SaveChangesAsync(ct) == 1;
    }

    public async Task<DeleteQuestionResultEnum> DeleteQuestionAsync(int idQuestion, int idLoggedUser, CancellationToken ct)
    {
        var question = await _appContext.Questions
                                    .Include(q => q.AppUsersQuestionInteractions)
                                    .Include(q => q.Answers)
                                    .SingleOrDefaultAsync(q => q.Id == idQuestion, ct);
        if (question is null)
            return DeleteQuestionResultEnum.ContentNotExist;
        if (!question.AppUsersQuestionInteractions.SingleOrDefault(qi => qi.IsCreator).IsCreator)
            return DeleteQuestionResultEnum.UserIsNotCreator;
        if (question.Answers.Count > 0)
            return DeleteQuestionResultEnum.QuestionHasAnswers;

        _appContext.Questions.Remove(question);
        return await _appContext.SaveChangesAsync(ct) == 0 ? DeleteQuestionResultEnum.UnSuccessful : DeleteQuestionResultEnum.Successful;
    }

    public async Task<bool> IsQuestionFollowedByUserAsync(int idQuestion, int idUser, CancellationToken ct)
    {
        var questionInteraction = await _appContext.AppUsersQuestionsInteractions.SingleOrDefaultAsync(qi => qi.QuestionId == idQuestion && qi.AppUserId == idUser, ct);
        return questionInteraction != null && questionInteraction.IsFollowing;
    }

    public async Task UpdateQuestionViewsCountAsync(int idQuestion, CancellationToken ct)
    {
        var question = await _appContext.Questions.SingleOrDefaultAsync(q => q.Id == idQuestion, ct);
        if (question is null)
            return;
        question.ViewsCount += 1;
        await _appContext.SaveChangesAsync(ct);
    }

    public async Task<QuestionDetailsEntity?> GetQuestionDetailsForUpdateAsync(int idQuestion, int idLoggedUser, CancellationToken ct)
    {
        return await _appContext.AppUsersQuestionsInteractions.Where(qi => qi.QuestionId == idQuestion && qi.AppUserId == idLoggedUser).Select(q => new QuestionDetailsEntity
        {
            Header = q.QuestionIdNavigation.Header,
            Content = q.QuestionIdNavigation.ContentIdNavigation.RawText,
            Tags = q.QuestionIdNavigation.TagsInQuestion.Select(t => new QuestionDetailsTag()
            {
                Id = t.TagId,
                Name = t.TagIdNavigation.Name
            })
        }).SingleOrDefaultAsync(ct);
    }

    public async Task<bool?> UpdateQuestionAsync(int idQuestion, int contentId, int userId, IEnumerable<(int order, int tagId)> tags, string title, string rawText, string text, IEnumerable<string> imageNames, DateTime creationTime, CancellationToken ct)
    {
        //create transaction 
        await using var ctx = await _appContext.Database.BeginTransactionAsync(ct);
        try
        {
            var amountOfDataToUpdate = 0;
            var question = await _appContext.Questions
                                        .Include(q => q.AppUsersQuestionInteractions)
                                        .Include(q => q.ContentIdNavigation)
                                        .Include(q => q.TagsInQuestion)
                                        .SingleOrDefaultAsync(q => q.Id == idQuestion, ct);
            if (question is null || !question.AppUsersQuestionInteractions.Any(q => q.IsCreator && q.AppUserId == userId))
                return null;

            //update content
            var content = new Presistence.AppDb.Models.Content
            {
                Id = contentId,
                RawText = rawText,
                Text = text,
                ContentType = ContentTypeEnum.Question
            };
            question.CreatedAt = creationTime;
            question.Header = title;
            amountOfDataToUpdate += 1;

            if (question.ContentIdNavigation.RawText != rawText || question.ContentIdNavigation.Text != text)
                amountOfDataToUpdate += 1;
            question.ContentIdNavigation.RawText = rawText;
            question.ContentIdNavigation.Text = text;

            //remove old images
            var imagesInContent = await _appContext.ImagesInContent.Include(i => i.ImageIdNavigation).Where(i => i.ContentId == contentId).ToListAsync(ct);
            foreach (var image in imagesInContent)
            {
                _appContext.Images.Remove(image.ImageIdNavigation);
                _appContext.ImagesInContent.Remove(image);
            }
            amountOfDataToUpdate += imagesInContent.Count * 2;

            //create images and connect to content
            foreach (var imageName in imageNames)
            {
                var image = new Presistence.AppDb.Models.Image
                {
                    URL = imageName
                };
                await _appContext.Images.AddAsync(image, ct);
                var imageInContent = new ImageInContent
                {
                    //ContentIdNavigation = content,
                    ContentId = contentId,
                    ImageIdNavigation = image,
                };
                await _appContext.ImagesInContent.AddAsync(imageInContent, ct);
            }
            amountOfDataToUpdate += imageNames.Count() * 2;

            //delete old tags connections
            foreach (var questionTag in question.TagsInQuestion)
            {
                _appContext.Remove(questionTag);
            }
            amountOfDataToUpdate += question.TagsInQuestion.Count;

            //create tags in question
            foreach (var tag in tags)
            {
                var tagInQuestion = new TagInQuestion
                {
                    TagId = tag.tagId,
                    Order = tag.order,
                    QuestionIdNavigation = question
                };
                await _appContext.TagsInQuestions.AddAsync(tagInQuestion, ct);
            }
            amountOfDataToUpdate += tags.Count();

            var rowsAdded = await _appContext.SaveChangesAsync(ct);
            if (rowsAdded != amountOfDataToUpdate)
            {
                await ctx.RollbackAsync(ct);
                return false;
            }
            await ctx.CommitAsync(ct);
            return true;
        }
        catch
        {
            await ctx.RollbackAsync(ct);
            return false;
        }
    }
}
