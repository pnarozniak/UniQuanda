using UniQuanda.Core.Domain.Enums;

namespace UniQuanda.Core.Application.Services
{
    public interface ICacheService
    {
        /// <summary>
        /// Gets object from cache
        /// </summary>
        /// <typeparam name="T">Type of object</typeparam>
        /// <param name="key">How to identify object</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns>Object type of T if found, default otherwise</returns>
        public Task<T?> GetFromCacheAsync<T>(string key, CancellationToken ct);
        /// <summary>
        /// Saves object to cache
        /// </summary>
        /// <typeparam name="T">Type of object to save</typeparam>
        /// <param name="key">How to identify object</param>
        /// <param name="value">value to save</param>
        /// <param name="duration">Enum that defines how long to store a object</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>True if saved, false otherwise</returns>
        public Task<bool> SaveToCacheAsync<T>(string key, T value, DurationEnum duration, CancellationToken ct);
    }
}
