using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using UniQuanda.Core.Application.Repositories;
using UniQuanda.Core.Application.Services;
using UniQuanda.Core.Domain.Entities.App;
using UniQuanda.Core.Domain.Enums;
using UniQuanda.Core.Domain.Utils;
using UniQuanda.Infrastructure.Presistence.AppDb;
using UniQuanda.Infrastructure.Presistence.AppDb.Models;
using UniQuanda.Infrastructure.Presistence.AuthDb;

namespace UniQuanda.Infrastructure.Repositories;

public class AppUserRepository : IAppUserRepository
{
    private readonly AuthDbContext _authContext;
    private readonly AppDbContext _appContext;
    private readonly ICacheService _cacheService;
    private readonly IServiceProvider _serviceProvider;

    private const int CorrectAnswerPointsForOwner = 5;
    private const int CorrectAnswerPointsForQuestionOwner = 2;

    public AppUserRepository(AuthDbContext authContext, AppDbContext appContext, ICacheService cacheService, IServiceProvider serviceProvider)
    {
        this._authContext = authContext;
        this._appContext = appContext;
        this._cacheService = cacheService;
        _serviceProvider = serviceProvider;
    }

    public async Task<string?> GetUserAvatarAsync(int uid, CancellationToken ct)
    {
        return await _appContext.AppUsers
            .Where(u => u.Id == uid)
            .Select(u => u.Avatar)
            .SingleOrDefaultAsync(ct);
    }

    public async Task<AppUserEntity?> GetUserProfileAsync(int uid, CancellationToken ct)
    {
        var cacheStatisticsKey = CacheKey.GetUserProfileStatisticsKey(uid);
        var cacheTopTagsKey = CacheKey.GetUserProfileTopTagsKey(uid);
        var cacheDuration = DurationEnum.UserProfileTopTags;

        var query = _appContext.AppUsers.Where(u => u.Id == uid);
        var cacheStatisticsResult = await _cacheService.GetFromCacheAsync<(int Points, int QuestionAmount, int AnswersAmount)>(cacheStatisticsKey, ct);
        var cacheTopTagsResult = await _cacheService.GetFromCacheAsync<IEnumerable<(string Tag, int Amount)>>(cacheTopTagsKey, ct);

        AppUserEntity? user = null;
        if (Equals(cacheStatisticsResult, default((int Points, int QuestionAmount, int AnswersAmount))) || Equals(cacheTopTagsResult, default(IEnumerable<(string Tag, int Amount)>)))
        {
            user = await query.Select(u => new AppUserEntity()
            {
                Id = u.Id,
                FirstName = u.FirstName,
                LastName = u.LastName,
                Nickname = u.Nickname,
                Avatar = u.Avatar,
                Banner = u.Banner,
                AboutText = u.AboutText,
                Birthdate = u.Birthdate,
                City = u.City,
                SemanticScholarProfile = u.SemanticScholarProfile,
                Contact = u.Contact,
                Titles = u.AppUserTitles
                    .Where(ut => ut.AppUserId == u.Id)
                    .Select(t => new AcademicTitleEntity()
                    {
                        Id = t.AcademicTitleId,
                        Name = t.AcademicTitleIdNavigation.Name,
                        AcademicTitleType = t.AcademicTitleIdNavigation.AcademicTitleType,
                        Order = t.Order
                    })
                    .ToList(),
                Universities = u.AppUserInUniversities
                    .Where(uu => uu.AppUserId == uid)
                    .Select(uu => new UniversityEntity()
                    {
                        Id = uu.UniversityId,
                        Name = uu.UniversityIdNavigation.Name,
                        Icon = uu.UniversityIdNavigation.Icon,
                        Order = uu.Order
                    }).ToList(),
                AnswersAmount = u.AppUserAnswersInteractions.Where(a => a.AppUserId == uid && a.IsCreator).Count(),
                QuestionsAmount = u.AppUserQuestionsInteractions.Where(q => q.AppUserId == uid && q.IsCreator).Count(),
                Points = _appContext.UsersPointsInTags.Where(p => p.AppUserId == uid).Sum(p => p.Points),
                Tags = u.UserPointsInTags.Where(t => t.AppUserId == uid)
                    .Select(t => new TagOnProfileEntity()
                    {
                        Name = t.TagIdNavigation.Name,
                        Points = t.Points
                    })
                    .OrderBy(t => t.Points)
                    .Take(3)
                    .ToList()
            }).SingleOrDefaultAsync(ct);

            if (Equals(user, default(AppUserEntity))) return null;

            (int Points, int QuestionAmount, int AnswersAmount) statistics = (user.Points ?? 0, user.QuestionsAmount ?? 0, user.AnswersAmount ?? 0);
            IEnumerable<(string Tag, int Amount)> topTags = user.Tags.Select(t => (t.Name, t.Points));
            await _cacheService.SaveToCacheAsync(cacheStatisticsKey, statistics, cacheDuration, ct);
            await _cacheService.SaveToCacheAsync(cacheTopTagsKey, topTags, cacheDuration, ct);
            return user;
        }
        else
        {
            user = await query.Select(u => new AppUserEntity()
            {
                Id = u.Id,
                FirstName = u.FirstName,
                LastName = u.LastName,
                Nickname = u.Nickname,
                Avatar = u.Avatar,
                Banner = u.Banner,
                AboutText = u.AboutText,
                Birthdate = u.Birthdate,
                City = u.City,
                SemanticScholarProfile = u.SemanticScholarProfile,
                Contact = u.Contact,
                Titles = u.AppUserTitles
                    .Where(ut => ut.AppUserId == u.Id)
                    .Select(t => new AcademicTitleEntity()
                    {
                        Id = t.AcademicTitleId,
                        Name = t.AcademicTitleIdNavigation.Name,
                        AcademicTitleType = t.AcademicTitleIdNavigation.AcademicTitleType,
                        Order = t.Order
                    })
                    .ToList(),
                Universities = u.AppUserInUniversities
                    .Where(uu => uu.AppUserId == uid)
                    .Select(uu => new UniversityEntity()
                    {
                        Id = uu.UniversityId,
                        Name = uu.UniversityIdNavigation.Name,
                        Logo = uu.UniversityIdNavigation.Logo,
                        Order = uu.Order
                    }).ToList()
            }).SingleAsync(ct);
            if (Equals(user, default(AppUserEntity))) return null;

            user.QuestionsAmount = cacheStatisticsResult.QuestionAmount;
            user.AnswersAmount = cacheStatisticsResult.AnswersAmount;
            user.Points = cacheStatisticsResult.Points;
            user.Tags = cacheTopTagsResult.Select(t => new TagOnProfileEntity()
            {
                Name = t.Tag,
                Points = t.Amount
            }).ToList();
            return user;
        }
    }

    public async Task<AppUserEntity?> GetAppUserByIdForProfileSettingsAsync(int idAppUser, CancellationToken ct)
    {
        var appUser = await _appContext.AppUsers
            .Where(u => u.Id == idAppUser)
            .Select(u => new AppUserEntity
            {
                Nickname = u.Nickname,
                FirstName = u.FirstName,
                LastName = u.LastName,
                Birthdate = u.Birthdate,
                Contact = u.Contact,
                City = u.City,
                AboutText = u.AboutText,
                Avatar = u.Avatar,
                Banner = u.Banner,
                SemanticScholarProfile = u.SemanticScholarProfile
            }).SingleOrDefaultAsync(ct);
        return appUser;
    }

    public async Task<bool?> UpdateAppUserAsync(AppUserEntity appUserEntity, bool isAvatarUpdated, bool isBannerUpdated, CancellationToken ct)
    {
        var appUser = await _appContext.AppUsers.SingleOrDefaultAsync(u => u.Id == appUserEntity.Id, ct);
        if (appUser is null)
            return null;

        var isNickNameToUpdate = appUser.Nickname != appUserEntity.Nickname;
        appUser.Nickname = appUserEntity.Nickname;
        appUser.FirstName = appUserEntity.FirstName;
        appUser.LastName = appUserEntity.LastName;
        appUser.City = appUserEntity.City;
        appUser.Contact = appUserEntity.Contact;
        appUser.Birthdate = appUserEntity.Birthdate;
        if (isAvatarUpdated)
            appUser.Avatar = appUserEntity.Avatar;
        if (isBannerUpdated)
            appUser.Banner = appUserEntity.Banner;
        appUser.SemanticScholarProfile = appUserEntity.SemanticScholarProfile;
        appUser.AboutText = appUserEntity.AboutText;

        if (!_appContext.ChangeTracker.HasChanges())
            return true;

        if (isNickNameToUpdate)
        {
            await using var tran = await _appContext.Database.BeginTransactionAsync(ct);
            try
            {
                var user = await _authContext.Users.SingleOrDefaultAsync(au => au.Id == appUserEntity.Id, ct);
                if (user is null)
                    return null;

                user.Nickname = appUserEntity.Nickname;
                if (await _authContext.SaveChangesAsync(ct) == 0)
                    return false;

                if (await _appContext.SaveChangesAsync(ct) == 0)
                    return false;

                await tran.CommitAsync(ct);
                return true;
            }
            catch (Exception exc)
            {
                await tran.RollbackAsync(ct);
                if (exc.InnerException is OperationCanceledException) throw;
                return false;
            }
        }
        else
        {
            if (await _appContext.SaveChangesAsync(ct) == 0)
                return false;
            return true;
        }
    }

    public async Task<bool?> IsNicknameUsedAsync(int uid, string nickname, CancellationToken ct)
    {
        var appUser = await _appContext.AppUsers.SingleOrDefaultAsync(au => au.Id == uid, ct);
        if (appUser is null)
            return null;
        if (appUser.Nickname == nickname)
            return false;

        return await _authContext.Users
            .AnyAsync(u => EF.Functions.ILike(u.Nickname, nickname), ct);
    }

    public async Task<bool> HasUserPremiumAsync(int idUser, CancellationToken ct)
    {
        var role = await _appContext.UserRoles.Where(
            ur => ur.AppUserId == idUser && ur.RoleIdNavigation.Name == AppRole.Premium)
            .SingleOrDefaultAsync(ct);
        if (role is null)
            return false;
        return role.ValidUnitl > DateTime.UtcNow;
    }

    public async Task UpdateAppUserPointsForLikeValueInTagsAsync(int idAnswer, int LikesIncreasedBy)
    {
        await using var scope = _serviceProvider.CreateAsyncScope();
        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        try
        {
            var toUpdate = await context.Answers.Where(d => d.Id == idAnswer).Select(a => new
            {
                IdAuthor = a.AppUsersAnswerInteractions.SingleOrDefault(ai => ai.IsCreator).AppUserIdNavigation.Id,
                Tags = a.ParentQuestionIdNavigation.TagsInQuestion.Select(t => t.TagId)

            }).SingleOrDefaultAsync();
            if (toUpdate == null)
                return;

            foreach (var tagId in toUpdate.Tags)
            {
                var userTagPoint = await context.UsersPointsInTags.SingleOrDefaultAsync(t => t.TagId == tagId && t.AppUserId == toUpdate.IdAuthor);
                if (userTagPoint != null)
                {
                    userTagPoint.Points += LikesIncreasedBy;
                }
                else
                {
                    var newUsersPointsInTags = new UserPointsInTag
                    {
                        AppUserId = toUpdate.IdAuthor,
                        TagId = tagId,
                        Points = LikesIncreasedBy
                    };
                    context.UsersPointsInTags.AddAsync(newUsersPointsInTags);
                }

            }
            await context.SaveChangesAsync();
        }
        catch
        {
            //Log.Error
        }
    }

    public async Task UpdateAppUserPointsForCorrectAnswerInTagsAsync(int idAnswer, int? idAuthorPrevCorrectAnswer)
    {
        await using var scope = _serviceProvider.CreateAsyncScope();
        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        try
        {
            var data = await context.Answers.Where(a => a.Id == idAnswer).Select(a => new
            {
                IdQuestion = a.ParentQuestionIdNavigation.Id,
                QuestionIdTags = a.ParentQuestionIdNavigation.TagsInQuestion.Select(t => t.TagId),
                IdQuestionAuthor = a.ParentQuestionIdNavigation.AppUsersQuestionInteractions.SingleOrDefault(qi => qi.IsCreator).AppUserIdNavigation.Id,
                IdAnswerAuthor = a.AppUsersAnswerInteractions.SingleOrDefault(ai => ai.IsCreator).AppUserIdNavigation.Id
            }).SingleOrDefaultAsync();
            if (data == null)
                return;

            if (idAuthorPrevCorrectAnswer == null || idAuthorPrevCorrectAnswer == -1)
            {
                var isAddingPoints = idAuthorPrevCorrectAnswer == null;

                var userAnswerPointTags = await context.UsersPointsInTags.Where(p => p.AppUserId == data.IdAnswerAuthor && data.QuestionIdTags.Any(d => d == p.TagId)).ToListAsync();
                var pointsForAnswerTag = isAddingPoints ? CorrectAnswerPointsForOwner : -CorrectAnswerPointsForOwner;
                foreach (var tagId in data.QuestionIdTags)
                {
                    var singlePointTag = userAnswerPointTags.SingleOrDefault(p => p.TagId == tagId);
                    if (singlePointTag != null)
                    {
                        singlePointTag.Points += pointsForAnswerTag;
                    }
                    else
                    {
                        var newUsersPointsInTags = new UserPointsInTag
                        {
                            AppUserId = data.IdAnswerAuthor,
                            TagId = tagId,
                            Points = pointsForAnswerTag
                        };
                        await context.UsersPointsInTags.AddAsync(newUsersPointsInTags);
                    }
                }

                var userQuestionPointTags = await context.UsersPointsInTags.Where(p => p.AppUserId == data.IdQuestionAuthor && data.QuestionIdTags.Any(d => d == p.TagId)).ToListAsync();
                var pointsForQuestionTag = isAddingPoints ? CorrectAnswerPointsForQuestionOwner : -CorrectAnswerPointsForQuestionOwner;
                foreach (var tagId in data.QuestionIdTags)
                {
                    var singlePointTag = userQuestionPointTags.SingleOrDefault(p => p.TagId == tagId);
                    if (singlePointTag != null)
                    {
                        singlePointTag.Points += pointsForQuestionTag;
                    }
                    else
                    {
                        var newUsersPointsInTags = new UserPointsInTag
                        {
                            AppUserId = data.IdQuestionAuthor,
                            TagId = tagId,
                            Points = pointsForQuestionTag
                        };
                        await context.UsersPointsInTags.AddAsync(newUsersPointsInTags);
                    }
                }

                await context.SaveChangesAsync();
                return;
            }
            var userAnswerPointTagsNew = await context.UsersPointsInTags.Where(p => p.AppUserId == data.IdAnswerAuthor && data.QuestionIdTags.Any(d => d == p.TagId)).ToListAsync();
            foreach (var tagId in data.QuestionIdTags)
            {
                var singlePointTag = userAnswerPointTagsNew.SingleOrDefault(p => p.TagId == tagId);
                if (singlePointTag != null)
                {
                    singlePointTag.Points += CorrectAnswerPointsForOwner;
                }
                else
                {
                    var newUsersPointsInTags = new UserPointsInTag
                    {
                        AppUserId = data.IdAnswerAuthor,
                        TagId = tagId,
                        Points = CorrectAnswerPointsForOwner
                    };
                    await context.UsersPointsInTags.AddAsync(newUsersPointsInTags);
                }
            }

            var userQuestionPointTagsNew = await context.UsersPointsInTags.Where(p => p.AppUserId == data.IdQuestionAuthor && data.QuestionIdTags.Any(d => d == p.TagId)).ToListAsync();
            foreach (var tagId in data.QuestionIdTags)
            {
                var singlePointTag = userQuestionPointTagsNew.SingleOrDefault(p => p.TagId == tagId);
                if (singlePointTag != null)
                {
                    singlePointTag.Points += CorrectAnswerPointsForQuestionOwner;
                }
                else
                {
                    var newUsersPointsInTags = new UserPointsInTag
                    {
                        AppUserId = data.IdQuestionAuthor,
                        TagId = tagId,
                        Points = CorrectAnswerPointsForQuestionOwner
                    };
                    await context.UsersPointsInTags.AddAsync(newUsersPointsInTags);
                }
            }

            var userAnswerPointTagsOld = await context.UsersPointsInTags.Where(p => p.AppUserId == idAuthorPrevCorrectAnswer && data.QuestionIdTags.Any(d => d == p.TagId)).ToListAsync();
            foreach (var tagPoint in userAnswerPointTagsNew)
                tagPoint.Points -= CorrectAnswerPointsForOwner;
            var userQuestionPointTagsOld = await context.UsersPointsInTags.Where(p => p.AppUserId == idAuthorPrevCorrectAnswer && data.QuestionIdTags.Any(d => d == p.TagId)).ToListAsync();
            foreach (var tagPoint in userQuestionPointTagsNew)
                tagPoint.Points -= CorrectAnswerPointsForQuestionOwner;
            return;
        }
        catch
        {
            //Log.Error
        }
    }
}