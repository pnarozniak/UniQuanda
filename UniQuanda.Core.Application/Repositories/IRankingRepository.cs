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
        ///     Gets given amount of users in global ranking
        /// </summary>
        /// <param name="take">How many records to take</param>
        /// <param name="skip">How many records to skip</param>
        /// <param name="ct"></param>
        /// <returns>List of users on given page</returns>
        public Task<IEnumerable<AppUserEntity>> GetGlobalRankingUsersAsync(int take, int skip, CancellationToken ct);

        /// <summary>
        ///     Gets global ranking table size
        /// </summary>
        /// <param name="ct"></param>
        /// <returns>Number of rows in table</returns>
        public Task<int> GetGlobalRankingCountAsync(CancellationToken ct);

        /// <summary>
        ///     Get amount of all users having points in tag
        /// </summary>
        /// <param name="tagId">Id of tag</param>
        /// <param name="ct"></param>
        /// <returns></returns>
        public Task<int> GetTagRankingCountAsync(int tagId, CancellationToken ct);

        /// <summary>
        ///     Gets page with max 10 users having points in given tag
        /// </summary>
        /// <param name="tagId">Id of tag to find users</param>
        /// <param name="page">Page number</param>
        /// <param name="pageSize">Amount of items on page</param>
        /// <param name="ct"></param>
        /// <returns>List of users on given page</returns>
        public Task<IEnumerable<AppUserEntity>> GetTagRankingUsersAsync(int tagId, int page, int pageSize, CancellationToken ct);
    }
}