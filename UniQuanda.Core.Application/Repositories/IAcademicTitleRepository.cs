using UniQuanda.Core.Domain.Entities.App;
using UniQuanda.Core.Domain.Enums;

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
        public Task<bool> AddAcademicTitleRequestForUserAsync(int id, int uid, string imageUrl, int titleId, string? additionalInfo, DateTime createdAt, CancellationToken ct);

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

        /// <summary>
        ///     Gets pending requests for academic titles using paging
        /// </summary>
        /// <param name="take">How many records to take</param>
        /// <param name="skip">How many first records to skip</param>
        /// <param name="ct"></param>
        /// <returns>Page of academic titles with status pending</returns>
        public Task<IEnumerable<AcademicTitleRequestEntity>> GetPendingRequestsAsync(int take, int skip, CancellationToken ct);

        /// <summary>
        ///     Gets amount of all requests for titles 
        /// </summary>
        /// <param name="ct"></param>
        /// <returns>Amount of all requests for academic titles that are pending</returns>
        public Task<int> GetPendingRequestsCountAsync(CancellationToken ct);

        /// <summary>
        ///     Gets Reuqest for academic title by id
        /// </summary>
        /// <param name="requestId">Request id</param>
        /// <param name="ct"></param>
        /// <returns>AcademicTitleRequestEntity if found. Null otherwise</returns>
        public Task<AcademicTitleRequestEntity?> GetRequestByIdAsync(int requestId, CancellationToken ct);

        /// <summary>
        ///     Assings status to request for repository
        /// </summary>
        /// <param name="requestId"></param>
        /// <param name="status"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        public Task<bool> AssingStatusToRequestForAcademicTitleAsync(int requestId, TitleRequestStatusEnum status, CancellationToken ct);

        /// <summary>
        ///     Assings academic title to user. If user has title with same type, it removes previous type.
        /// </summary>
        /// <param name="userId">User id</param>
        /// <param name="titleId">Title id</param>
        /// <param name="order">Order to display. If null, then title will be displayed as last</param>
        /// <param name="ct"></param>
        /// <returns></returns>
        public Task<bool> SetAcademicTitleToUserAsync(int userId, int titleId, int? order, CancellationToken ct);
    }
}
