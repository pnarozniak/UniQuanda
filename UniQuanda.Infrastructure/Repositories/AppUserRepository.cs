using Microsoft.EntityFrameworkCore;
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
        var cacheKey = CacheKey.GetUserProfileStatistics(uid);
        var cacheDuration = DurationEnum.ThreeHours;

        var query = _appContext.AppUsers.Where(u => u.Id == uid);
        var cacheResult = await _cacheService.GetFromCacheAsync<(int Points, int QuestionAmount, int AnswersAmount)>(cacheKey, ct);
        AppUserEntity? user = null;
        if (Equals(cacheResult, default((int Points, int QuestionAmount, int AnswersAmount))))
        {
            user = await query.Select(u => new AppUserEntity()
            {
                Id = u.Id,
                FirstName = u.FirstName,
                LastName = u.LastName,
                Nickname = u.Nickname,
                Avatar = u.Avatar,
                Banner = u.Banner,
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
            }).SingleOrDefaultAsync(ct);

            if (Equals(user, default(AppUserEntity))) return null;

            (int Points, int QuestionAmount, int AnswersAmount) statistics = (user.Points ?? 0, user.QuestionsAmount ?? 0, user.AnswersAmount ?? 0);
            await _cacheService.SetToCacheAsync(cacheKey, statistics, cacheDuration, ct);
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

            user.QuestionsAmount = cacheResult.QuestionAmount;
            user.AnswersAmount = cacheResult.AnswersAmount;
            user.Points = cacheResult.Points;
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

    public async Task<AppUserUpdateResult> UpdateAppUserAsync(AppUserEntity appUserEntity, CancellationToken ct)
    {
        var appUser = await _appContext.AppUsers.SingleOrDefaultAsync(u => u.Id == appUserEntity.Id, ct);
        if (appUser is null)
            return new AppUserUpdateResult { IsSuccessful = null };

        var isNickNameToUpdate = appUser.Nickname != appUserEntity.Nickname;
        appUser.Nickname = appUserEntity.Nickname;
        appUser.FirstName = appUserEntity.FirstName;
        appUser.LastName = appUserEntity.LastName;
        appUser.City = appUserEntity.City;
        appUser.PhoneNumber = appUserEntity.PhoneNumber;
        appUser.Birthdate = appUserEntity.Birthdate;
        appUser.Avatar = appUserEntity.Avatar;
        appUser.Banner = appUserEntity.Banner;
        appUser.SemanticScholarProfile = appUserEntity.SemanticScholarProfile;
        appUser.AboutText = appUserEntity.AboutText;

        if (!_appContext.ChangeTracker.HasChanges())
            return new AppUserUpdateResult { IsSuccessful = true };

        if (isNickNameToUpdate)
        {
            await using var tran = await _appContext.Database.BeginTransactionAsync(ct);
            try
            {
                var user = await _authContext.Users.SingleOrDefaultAsync(au => au.Id == appUserEntity.Id, ct);
                if (user is null)
                    return new AppUserUpdateResult { IsSuccessful = null };

                user.Nickname = appUserEntity.Nickname;
                if (await _authContext.SaveChangesAsync(ct) == 0)
                    return new AppUserUpdateResult { IsSuccessful = false };

                if (await _appContext.SaveChangesAsync(ct) == 0)
                    return new AppUserUpdateResult { IsSuccessful = false };

                await tran.CommitAsync(ct);
                return new AppUserUpdateResult { IsSuccessful = true, AvatarUrl = appUser.Avatar };
            }
            catch (Exception exc)
            {
                await tran.RollbackAsync(ct);
                if (exc.InnerException is OperationCanceledException) throw;
                return new AppUserUpdateResult { IsSuccessful = false };
            }
        }
        else
        {
            if (await _appContext.SaveChangesAsync(ct) == 0)
                return new AppUserUpdateResult { IsSuccessful = false };
            return new AppUserUpdateResult { IsSuccessful = true, AvatarUrl = appUser.Avatar };
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

    private static bool IsDataToUpdate(AppUserEntity newAppUser, AppUser oldAppUser)
    {
        return !(newAppUser.FirstName == oldAppUser.FirstName &&
            newAppUser.LastName == oldAppUser.LastName &&
            newAppUser.City == oldAppUser.City &&
            newAppUser.PhoneNumber == oldAppUser.PhoneNumber &&
            newAppUser.Birthdate == oldAppUser.Birthdate &&
            newAppUser.SemanticScholarProfile == oldAppUser.SemanticScholarProfile &&
            newAppUser.AboutText == oldAppUser.AboutText &&
            newAppUser.Avatar == oldAppUser.Avatar &&
            newAppUser.Banner == oldAppUser.Banner);
    }
}