using Microsoft.EntityFrameworkCore;
using UniQuanda.Core.Application.Repositories;
using UniQuanda.Core.Application.Services;
using UniQuanda.Core.Domain.Entities.App;
using UniQuanda.Core.Domain.Enums;
using UniQuanda.Infrastructure.Presistence.AppDb;

namespace UniQuanda.Infrastructure.Repositories;

public class RankingRepository : IRankingRepository
{
    private readonly AppDbContext _appContext;
    private readonly ICacheService _cacheService;

    public RankingRepository(AppDbContext appContext, ICacheService cacheService)
    {
        this._appContext = appContext;
        this._cacheService = cacheService;
    }

    public async Task<int> GetGlobalRankingCountAsync(CancellationToken ct)
    {
        return await _appContext.GlobalRankings.CountAsync(ct);
    }

    public async Task<IEnumerable<AppUserEntity>> GetGlobalRankingUsersAsync(int take, int skip, CancellationToken ct)
    {
        return await _appContext.GlobalRankings
            .Select(gr => new AppUserEntity()
            {
                Id = gr.AppUserId,
                Nickname = gr.AppUserIdNavigation.Nickname,
                Titles = gr.AppUserIdNavigation.AppUserTitles.Select(t => new AcademicTitleEntity()
                {
                    Name = t.AcademicTitleIdNavigation.Name,
                    AcademicTitleType = t.AcademicTitleIdNavigation.AcademicTitleType,
                    Order = t.Order
                }),
                PlaceInRanking = gr.Place,
                Points = gr.Points,
                Avatar = gr.AppUserIdNavigation.Avatar
            }).Skip(skip).Take(take)
            .ToListAsync(ct);
    }

    public async Task<int> GetTagRankingCountAsync(int tagId, CancellationToken ct)
    {
        return await _appContext.UsersPointsInTags.Where(t => t.TagId == tagId).CountAsync(ct);
    }

    public async Task<IEnumerable<AppUserEntity>> GetTagRankingUsersAsync(int tagId, int page, int pageSize, CancellationToken ct)
    {
        var cacheKey = CacheKey.GetTagRankingKey(tagId, page);
        var cacheResult = await _cacheService.GetFromCacheAsync<IEnumerable<AppUserEntity>>(cacheKey, ct);
        if (Equals(cacheResult, default(IEnumerable<AppUserEntity>)))
        {
            var take = pageSize;
            var skip = (page - 1) * pageSize;
            var result = await _appContext.UsersPointsInTags.Where(t => t.TagId == tagId)
                .OrderByDescending(t => t.Points)
                .Select((t) => new AppUserEntity()
                {
                    Id = t.AppUserId,
                    Nickname = t.AppUserIdNavigation.Nickname,
                    Titles = t.AppUserIdNavigation.AppUserTitles.Select(t => new AcademicTitleEntity()
                    {
                        Name = t.AcademicTitleIdNavigation.Name,
                        AcademicTitleType = t.AcademicTitleIdNavigation.AcademicTitleType,
                        Order = t.Order
                    }),
                    Points = t.Points,
                    Avatar = t.AppUserIdNavigation.Avatar,
                })
                .Skip(skip).Take(take).ToListAsync(ct);

            var maxIndex = pageSize > result.Count ? result.Count : pageSize;
            for (int i = 0; i < maxIndex; i++)
            {
                result[i].PlaceInRanking = skip + i + 1;
            }


            var secondsToMidnight = (int)(new TimeSpan(24, 0, 0) - DateTime.Now.TimeOfDay).TotalSeconds;
            await _cacheService.SaveToCacheAsync<IEnumerable<AppUserEntity>>(cacheKey, result, DurationEnum.UntilMidnight, ct);
            return result;

        }
        else
        {
            return cacheResult;
        }
    }

    public async Task<IEnumerable<AppUserEntity>> GetTop5UsersAsync(CancellationToken ct)
    {
        return await _appContext.UsersPointsInTags
            .GroupBy(up => new { up.AppUserId })
            .Select(gr => new
            {
                Id = gr.Key.AppUserId,
                Points = gr.Sum(g => g.Points)
            })
            .OrderByDescending(u => u.Points)
            .Take(5)
            .Join(_appContext.AppUsers, gr => gr.Id, appUser => appUser.Id, (gr, appUser) => new AppUserEntity
            {
                Id = gr.Id,
                Nickname = appUser.Nickname,
                Avatar = appUser.Avatar,
                Points = gr.Points
            })
            .ToListAsync(ct);
    }

    public async Task<IEnumerable<AppUserEntity>?> GetTop5UsersFromCacheAsync(CancellationToken ct)
    {
        var cacheKey = CacheKey.GetTop5UsersKey();
        var top5users = await _cacheService.GetFromCacheAsync<IEnumerable<AppUserEntity>>(cacheKey, ct);
        return top5users;
    }

    public async Task SaveTop5UsersToCacheAsync(IEnumerable<AppUserEntity> top5users, CancellationToken ct)
    {
        var cacheKey = CacheKey.GetTop5UsersKey();
        await _cacheService.SaveToCacheAsync<IEnumerable<AppUserEntity>>(cacheKey, top5users, DurationEnum.Top5Users, ct);
    }
}