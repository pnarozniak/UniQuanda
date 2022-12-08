using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using UniQuanda.Core.Application.Services;
using UniQuanda.Core.Domain.Enums;

namespace UniQuanda.Infrastructure.Services;

public class CacheService : ICacheService
{
    public IDistributedCache _context;

    public CacheService(IDistributedCache context)
    {
        _context = context;
    }

    public async Task<T?> GetFromCacheAsync<T>(string key, CancellationToken ct)
    {
        var result = await _context.GetStringAsync(key, ct);
        return result != null ? JsonConvert.DeserializeObject<T>(result) : default;
    }

    public async Task<bool> SaveToCacheAsync<T>(string key, T value, DurationEnum duration, CancellationToken ct)
    {
        try
        {
            var options = new DistributedCacheEntryOptions();
            options.SetSlidingExpiration(duration.GetTimeSpan());

            await _context.SetStringAsync(key, JsonConvert.SerializeObject(value), options, ct);
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
}