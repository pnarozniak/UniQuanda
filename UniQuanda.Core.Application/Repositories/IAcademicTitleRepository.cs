using UniQuanda.Core.Domain.Entities.App;

namespace UniQuanda.Core.Application.Repositories
{
    public interface IAcademicTitleRepository
    {
        /// <summary>
        ///     Gets all requested titles by user with their statuses.
        /// </summary>
        /// <param name="uid">Id of user</param>
        /// <param name="ct"></param>
        /// <returns></returns>
        public Task<IEnumerable<AcademicTitleRequestEntity>> GetRequestedTitlesOfUserAsync(int uid, CancellationToken ct);

        /// <summary>
        ///     Gets all assigned titles to user, with their order
        /// </summary>
        /// <param name="uid">User id</param>
        /// <param name="ct"></param>
        /// <returns></returns>
        public Task<IEnumerable<AcademicTitleEntity>> GetAcademicTitlesOfUserAsync(int uid, CancellationToken ct);

        /// <summary>
        ///     Saves order of titles in user profile.
        /// </summary>
        /// <param name="uid">user id</param>
        /// <param name="orders">Dictionary where key is order and value is title id</param>
        /// <param name="ct"></param>
        /// <returns></returns>
        public Task<bool> SaveOrderOfAcademicTitleForUserAsync(int uid, IDictionary<int, int> orders, CancellationToken ct);

        /// <summary>
        ///     Adds request for title to user
        /// </summary>
        /// <param name="id">Request ID - should be unique (use function GetNextTitleRequestIdAsync)</param>
        /// <param name="uid">User id</param>
        /// <param name="imageUrl">Url to image</param>
        /// <param name="titleId">Id of title to assign</param>
        /// <param name="additionalInfo">Additional infformation provided by user</param>
        /// <param name="createdAt">Time of creation</param>
        /// <param name="ct"></param>
        /// <returns></returns>
        public Task<bool> AddAcademicTitleRequestForUserAsync(int id,int uid, string imageUrl, int titleId, string? additionalInfo, DateTime createdAt, CancellationToken ct);

        /// <summary>
        ///    Gets all academic titles that have Type != Academic
        /// </summary>
        /// <param name="ct"></param>
        /// <returns></returns>
        public Task<IEnumerable<AcademicTitleEntity>> GetRequestableAcademicTitlesAsync(CancellationToken ct);

        /// <summary>
        ///     Gets next sequence number for AcademicTitleRequest
        /// </summary>
        /// <param name="ct"></param>
        /// <returns>Id of next content</returns>
        public Task<int> GetNextTitleRequestIdAsync(CancellationToken ct);
    }
}
