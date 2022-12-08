using UniQuanda.Core.Domain.Entities.App;

namespace UniQuanda.Core.Application.Repositories
{
    public interface IRankingRepository
    {
        /// <summary>
        ///     Gets top 5 users with highest amount of points from db
        /// </summary>
        /// <param name="ct">Operation cancellation token</param>
        /// <returns>Collection with AppUserEntity up to 5 elements</returns>
        public Task<IEnumerable<AppUserEntity>> GetTop5UsersAsync(CancellationToken ct);

        /// <summary>
        ///     Gets top 5 users with highest amount of points from cache
        /// </summary>
        /// <param name="ct">Operation cancellation token</param>
        /// <returns>Collection with AppUserEntity up to 5 elements if found in cache, otherwise Null</returns>
        public Task<IEnumerable<AppUserEntity>?> GetTop5UsersFromCacheAsync(CancellationToken ct);

        /// <summary>
        ///     Saves top 5 users with highest amount of points to cache
        /// </summary>
        /// <param name="top5users">Top 5 users to be saved</param>
        /// <param name="ct">Operation cancellation token</param>
        public Task SaveTop5UsersToCacheAsync(IEnumerable<AppUserEntity> top5users, CancellationToken ct);
    }
}