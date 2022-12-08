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