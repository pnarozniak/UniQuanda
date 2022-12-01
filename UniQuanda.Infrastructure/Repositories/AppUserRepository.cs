using Microsoft.EntityFrameworkCore;
using UniQuanda.Core.Application.Repositories;
using UniQuanda.Core.Application.Services;
using UniQuanda.Core.Domain.Entities.App;
using UniQuanda.Core.Domain.Enums;
using UniQuanda.Infrastructure.Presistence.AppDb;
using UniQuanda.Infrastructure.Presistence.AuthDb;

namespace UniQuanda.Infrastructure.Repositories;

public class AppUserRepository : IAppUserRepository
{
    private readonly AuthDbContext _authContext;
    private readonly AppDbContext _appContext;
    private readonly ICacheService _cacheService;

    public AppUserRepository(AuthDbContext authContext, AppDbContext appContext, ICacheService cacheService)
    {
        this._authContext = authContext;
        this._appContext = appContext;
        this._cacheService = cacheService;
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
        var cacheStatisticsKey = CacheKey.GetUserProfileStatistics(uid);
        var cacheTopTagsKey = CacheKey.GetUserProfileTopTags(uid);
        var cacheDuration = DurationEnum.ThreeHours;

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
                PhoneNumber = u.PhoneNumber,
                Titles = u.AppUserTitles
                    .Where(ut => ut.AppUserId == u.Id)
                    .Select(t => new AcademicTitleEntity()
                    {
                        Id = t.AcademicTitleId,
                        Name = t.AcademicTitleIdNavigation.Name,
                        Type = t.AcademicTitleIdNavigation.AcademicTitleType,
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
            await _cacheService.SetToCacheAsync(cacheStatisticsKey, statistics, cacheDuration, ct);
            await _cacheService.SetToCacheAsync(cacheTopTagsKey, topTags, cacheDuration, ct);
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
                PhoneNumber = u.PhoneNumber,
                Titles = u.AppUserTitles
                    .Where(ut => ut.AppUserId == u.Id)
                    .Select(t => new AcademicTitleEntity()
                    {
                        Id = t.AcademicTitleId,
                        Name = t.AcademicTitleIdNavigation.Name,
                        Type = t.AcademicTitleIdNavigation.AcademicTitleType,
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
                PhoneNumber = u.PhoneNumber,
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
        appUser.PhoneNumber = appUserEntity.PhoneNumber;
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
}