using Microsoft.EntityFrameworkCore;
using UniQuanda.Core.Application.Repositories;
using UniQuanda.Core.Application.Services;
using UniQuanda.Core.Domain.Entities;
using UniQuanda.Core.Domain.Entities.App;
using UniQuanda.Core.Domain.Enums;
using UniQuanda.Infrastructure.Presistence.AppDb;


namespace UniQuanda.Infrastructure.Repositories;

public class AppUserRepository : IAppUserRepository
{
    private readonly AppDbContext _appContext;
    private readonly ICacheService _cacheService;

    public AppUserRepository(AppDbContext appContext, ICacheService _cacheService)
    {
        _appContext = appContext;
        this._cacheService = _cacheService;
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
        var cacheKey = "user-profile-statistics";
        var cacheDuration = DurationEnum.THREE_HOURS;

        var query = _appContext.AppUsers.Where(u => u.Id == uid);
        var cacheResult = await _cacheService.GetFromCache<(int Points, int QuestionAmount, int AnswersAmount)>($"{cacheKey}-{uid}", ct);
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
            await _cacheService.SetToCache($"{cacheKey}-{uid}", statistics, cacheDuration, ct);
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
}