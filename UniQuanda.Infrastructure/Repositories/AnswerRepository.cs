using Microsoft.EntityFrameworkCore;
using UniQuanda.Core.Application.Repositories;
using UniQuanda.Core.Domain.Entities.App;
using UniQuanda.Core.Domain.Enums;
using UniQuanda.Core.Domain.ValueObjects;
using UniQuanda.Infrastructure.Presistence.AppDb;
using UniQuanda.Infrastructure.Presistence.AppDb.Models;

namespace UniQuanda.Infrastructure.Repositories;

public class AnswerRepository : IAnswerRepository
{
    private readonly AppDbContext _appContext;
    private const int PageSize = 5;

    public AnswerRepository(AppDbContext appContext)
    {
        _appContext = appContext;
    }

    public async Task<IEnumerable<AnswerEntity>> GetAnswersOfUserAsync(int userId, int take, int skip, CancellationToken ct)
    {
        return await _appContext.AppUsersAnswersInteractions.Where(
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
                    Tags = aai.AnswerIdNavigation.ParentQuestionIdNavigation.TagsInQuestion.Select(tiq => new TagEntity()
                    {
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
        return await _appContext.AppUsersAnswersInteractions.Where(aai => aai.IsCreator && aai.AppUserId == userId).CountAsync(ct);
    }

    public async Task<(bool isSuccessful, int? idAnswer)> AddAnswerAsync(int idContent, int idQuestion, int? parentQuestionId, int idUser, string rawText, string text, IEnumerable<string> imageNames, DateTime creationTime, CancellationToken ct)
    {
        await using var ctx = await _appContext.Database.BeginTransactionAsync(ct);
        try
        {
            //create content
            var content = new Presistence.AppDb.Models.Content
            {
                Id = idContent,
                RawText = rawText,
                Text = text,
                ContentType = ContentTypeEnum.Answer
            };
            await _appContext.Contents.AddAsync(content, ct);

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
                    ContentIdNavigation = content,
                    ImageIdNavigation = image,
                };
                await _appContext.ImagesInContent.AddAsync(imageInContent, ct);
            }
            // create answer
            var answer = new Answer
            {
                ContentId = idContent,
                CreatedAt = creationTime,
                ParentAnswerId = parentQuestionId,
                ParentQuestionId = idQuestion
            };
            await _appContext.Answers.AddAsync(answer, ct);

            //add creator interaction with answer
            var creatorInteraction = new AppUserAnswerInteraction
            {
                AppUserId = idUser,
                IsCreator = true,
                AnswerIdNavigation = answer
            };

            await _appContext.AppUsersAnswersInteractions.AddAsync(creatorInteraction, ct);

            var totalCount = imageNames.Count() * 2 + 3;
            var rowsAdded = await _appContext.SaveChangesAsync(ct);
            if (rowsAdded != totalCount)
            {
                await ctx.RollbackAsync(ct);
                return (isSuccessful: false, idAnswer: null);
            }
            await ctx.CommitAsync(ct);
            return (isSuccessful: true, idAnswer: answer.Id);
        }
        catch (Exception ex)
        {
            await ctx.RollbackAsync(ct);
            return (isSuccessful: false, idAnswer: null);
        }
    }

    public async Task<IEnumerable<AnswerDetails>> GetQuestionAnswersAsync(int idQuestion, int page, int? idComment, int? idLoggedUser, CancellationToken ct)
    {
        int numberOfComments = 3;
        if(idComment != null)
            numberOfComments = await _appContext.Answers.Where(a => a.Id == idComment && a.ParentQuestionId == idQuestion).Select(a => a.ParentAnswerIdNavigation.Comments.Count ).SingleOrDefaultAsync(ct);
            
        return await _appContext.Answers.Where(a => a.ParentQuestionId == idQuestion && a.ParentAnswerId == null).OrderByDescending(a => a.IsCorrect).ThenByDescending(a => a.LikeCount).Select(a => new AnswerDetails
        {
            Id = a.Id,
            ParentId = a.ParentAnswerId,
            IsModified = a.HasBeenModified,
            PublishDate = a.CreatedAt,
            IsCorrect = a.IsCorrect,
            Likes = a.LikeCount,
            Content = a.ContentIdNavigation.RawText,
            CommentsAmount = a.Comments.Count,
            UserLikeValue = idLoggedUser == null ? 0 : a.AppUsersAnswerInteractions.Where(ai => ai.AppUserId == idLoggedUser).Select(ai => ai.LikeValue).SingleOrDefault(),
            Author = a.AppUsersAnswerInteractions.Where(ai => ai.IsCreator).Select(au => new AuthorContent
            {
                Id = au.AppUserIdNavigation.Id,
                Nickname = au.AppUserIdNavigation.Nickname,
                AvatarUrl = au.AppUserIdNavigation.Avatar,
                AcademicTitles = au.AppUserIdNavigation.AppUserTitles.Select(t => new AcademicTitleEntity
                {
                    Name = t.AcademicTitleIdNavigation.Name,
                    Order = t.Order,
                    AcademicTitleType = t.AcademicTitleIdNavigation.AcademicTitleType
                })
            }).SingleOrDefault()!,
            Comments = a.Comments.OrderBy(c => c.LikeCount).Take(idComment != null ? numberOfComments : 3).Select(c => new AnswerDetails
            {
                Id = c.Id,
                ParentId = c.ParentAnswerId,
                IsModified = c.HasBeenModified,
                PublishDate = c.CreatedAt,
                Likes = c.LikeCount,
                Content = c.ContentIdNavigation.RawText,
                UserLikeValue = idLoggedUser == null ? 0 : c.AppUsersAnswerInteractions.Where(ai => ai.AppUserId == idLoggedUser).Select(ai => ai.LikeValue).SingleOrDefault(),
                Author = c.AppUsersAnswerInteractions.Where(ai => ai.IsCreator).Select(au => new AuthorContent
                {
                    Id = au.AppUserIdNavigation.Id,
                    Nickname = au.AppUserIdNavigation.Nickname,
                    AvatarUrl = au.AppUserIdNavigation.Avatar,
                    AcademicTitles = au.AppUserIdNavigation.AppUserTitles.Select(t => new AcademicTitleEntity
                    {
                        Name = t.AcademicTitleIdNavigation.Name,
                        Order = t.Order,
                        AcademicTitleType = t.AcademicTitleIdNavigation.AcademicTitleType
                    })
                }).SingleOrDefault()!
            })
        }).Skip((page - 1) * PageSize).Take(PageSize).ToListAsync(ct);
    }

    public async Task<(bool? isSuccess, int? idAuthorPrevCorrectAnswer)> MarkAnswerAsCorrectAsync(int idAnswer, int idLoggedUser, CancellationToken ct)
    {
        var answer = await _appContext.Answers.SingleOrDefaultAsync(a => a.Id == idAnswer, ct);
        if (answer == null || answer.ParentAnswerId != null)
            return (isSuccess: null, idAuthorPrevCorrectAnswer: null);
        if (!await _appContext.Answers.AnyAsync(a => a.ParentQuestionIdNavigation.AppUsersQuestionInteractions.Any(qi => qi.Id == idLoggedUser && qi.IsCreator), ct))
            return (isSuccess: null, idAuthorPrevCorrectAnswer: null);
        var prevCorrectAnswer = await _appContext.Answers.SingleOrDefaultAsync(a => a.ParentQuestionId == answer.ParentQuestionId && a.IsCorrect, ct);
        if (prevCorrectAnswer == null)
        {
            answer.IsCorrect = true;
            return (isSuccess: await _appContext.SaveChangesAsync(ct) == 1, idAuthorPrevCorrectAnswer: null);
        }
        else if (answer.Id == prevCorrectAnswer.Id)
        {
            answer.IsCorrect = false;
            return (isSuccess: await _appContext.SaveChangesAsync(ct) == 1, idAuthorPrevCorrectAnswer: -1);
        }
        prevCorrectAnswer.IsCorrect = false;
        answer.IsCorrect = true;
        var prevAnswerInteraction = await _appContext.AppUsersAnswersInteractions.SingleOrDefaultAsync(ai => ai.AnswerId == idAnswer && ai.IsCreator, ct);
        if (prevAnswerInteraction == null)
            return (isSuccess: null, idAuthorPrevCorrectAnswer: null);
        return (isSuccess: await _appContext.SaveChangesAsync(ct) == 2, idAuthorPrevCorrectAnswer: prevAnswerInteraction.AppUserId);
    }

    public async Task<bool?> UpdateAnswerAsync(int idContent, int idAnswer, int idUser, string rawText, string text, IEnumerable<string> imageNames, DateTime creationTime, CancellationToken ct)
    {
        await using var ctx = await _appContext.Database.BeginTransactionAsync(ct);
        try
        {
            var isOwnerAnswer = await _appContext.AppUsersAnswersInteractions.AnyAsync(ai => ai.AnswerId == idAnswer && ai.AppUserId == idUser && ai.IsCreator, ct);
            if (!isOwnerAnswer)
                return false;
            var answer = await _appContext.Answers
                                        .Include(a => a.ContentIdNavigation)
                                        .SingleOrDefaultAsync(a => a.Id == idAnswer, ct);
            if (answer == null)
                return null;

            var amountOfDataToUpdate = 0;
            //update content
            if (answer.ContentIdNavigation.RawText != rawText || answer.ContentIdNavigation.Text != text)
                amountOfDataToUpdate += 1;
            answer.ContentIdNavigation.RawText = rawText;
            answer.ContentIdNavigation.Text = text;

            answer.CreatedAt = creationTime;
            amountOfDataToUpdate += 1;

            var imagesInContent = await _appContext.ImagesInContent.Include(i => i.ImageIdNavigation).Where(i => i.ContentId == idContent).ToListAsync(ct);
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
                    ContentId = idContent,
                    ImageIdNavigation = image,
                };
                await _appContext.ImagesInContent.AddAsync(imageInContent, ct);
            }
            amountOfDataToUpdate += imageNames.Count() * 2;

            if (await _appContext.SaveChangesAsync(ct) != amountOfDataToUpdate)
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

    public async Task<UpdateAnswerLikeValueEntity> UpdateAnswerLikeValueAsync(int idAnswer, int likeValue, int idLoggedUser, CancellationToken ct)
    {
        var answer = await _appContext.Answers.Include(a => a.AppUsersAnswerInteractions).SingleOrDefaultAsync(a => a.Id == idAnswer, ct);
        if (answer == null || answer.AppUsersAnswerInteractions.Any(ai => ai.AppUserId == idLoggedUser && ai.AnswerId == idAnswer))
            return new UpdateAnswerLikeValueEntity { IsUpdateSuccessful = null };

        var answerInteraction = await _appContext.AppUsersAnswersInteractions.SingleOrDefaultAsync(ai => ai.AppUserId == idLoggedUser && ai.AnswerId == idAnswer, ct);
        if (answerInteraction == null)
        {
            var newAnswerInteraction = new AppUserAnswerInteraction
            {
                AnswerId = idAnswer,
                AppUserId = idLoggedUser,
                LikeValue = likeValue,
            };
            await _appContext.AppUsersAnswersInteractions.AddAsync(newAnswerInteraction, ct);
            answer.LikeCount += likeValue;

            return new UpdateAnswerLikeValueEntity
            {
                IsUpdateSuccessful = await _appContext.SaveChangesAsync(ct) == 2,
                LikeValue = likeValue,
                Likes = answer.LikeCount,
                LikesIncreasedBy = likeValue
            };
        }
        else if (answerInteraction.LikeValue == likeValue)
        {
            answerInteraction.LikeValue = 0;
            answer.LikeCount -= likeValue;
            return new UpdateAnswerLikeValueEntity
            {
                IsUpdateSuccessful = await _appContext.SaveChangesAsync(ct) == 2,
                LikeValue = 0,
                Likes = answer.LikeCount,
                LikesIncreasedBy = -likeValue
            };
        }
        var oldLikeValue = answerInteraction.LikeValue;
        answerInteraction.LikeValue = likeValue;
        var newLikeValueToAdd = Math.Abs(oldLikeValue - likeValue) == 2 ? likeValue * 2 : likeValue;
        answer.LikeCount += newLikeValueToAdd;
        return new UpdateAnswerLikeValueEntity
        {
            IsUpdateSuccessful = await _appContext.SaveChangesAsync(ct) == 2,
            LikeValue = likeValue,
            Likes = answer.LikeCount,
            LikesIncreasedBy = newLikeValueToAdd
        };
    }

    public async Task<bool?> DeleteAnswerAsync(int idAnswer, int idLoggedUser, CancellationToken ct)
    {
        var answer = await _appContext.Answers.Include(a => a.Comments).Include(a => a.AppUsersAnswerInteractions).SingleOrDefaultAsync(a => a.Id == idAnswer, ct);
        if (answer == null)
            return null;
        if (answer.IsCorrect || answer.LikeCount < 0 || !answer.AppUsersAnswerInteractions.Any(ai => ai.IsCreator && ai.AppUserId == idLoggedUser))
            return false;

        var amountOfDeletedData = 2 + answer.Comments.Count;
        _appContext.Remove(answer);
        return await _appContext.SaveChangesAsync(ct) == amountOfDeletedData;
    }

    public async Task<IEnumerable<AnswerDetails>> GetAllCommentsAsync(int idParentAnswer, int? idLoggedUser, CancellationToken ct)
    {
        return await _appContext.Answers.Where(c => c.ParentAnswerId == idParentAnswer).Select(c => new AnswerDetails
        {
            Id = c.Id,
            ParentId = c.ParentAnswerId,
            IsModified = c.HasBeenModified,
            PublishDate = c.CreatedAt,
            Likes = c.LikeCount,
            Content = c.ContentIdNavigation.RawText,
            UserLikeValue = idLoggedUser == null ? 0 : c.AppUsersAnswerInteractions.Where(ai => ai.AppUserId == idLoggedUser).Select(ai => ai.LikeValue).SingleOrDefault(),
            Author = c.AppUsersAnswerInteractions.Where(ai => ai.IsCreator).Select(au => new AuthorContent
            {
                Id = au.AppUserIdNavigation.Id,
                Nickname = au.AppUserIdNavigation.Nickname,
                AvatarUrl = au.AppUserIdNavigation.Avatar,
                AcademicTitles = au.AppUserIdNavigation.AppUserTitles.Select(t => new AcademicTitleEntity
                {
                    Name = t.AcademicTitleIdNavigation.Name,
                    Order = t.Order,
                    AcademicTitleType = t.AcademicTitleIdNavigation.AcademicTitleType
                })
            }).SingleOrDefault()!
        }).ToListAsync(ct);
    }

    public async Task<int> GetAnswerPageAsync(int idQuestion, int idAnswer, CancellationToken ct)
    {
        var answers = await _appContext.Answers.Where(a => a.ParentQuestionId == idQuestion && a.ParentAnswerId == null).OrderByDescending(a => a.IsCorrect).ThenByDescending(a => a.LikeCount).ToListAsync(ct);
        return (answers.FindIndex(a => a.Id == idAnswer) / PageSize) + 1;
    }
}
