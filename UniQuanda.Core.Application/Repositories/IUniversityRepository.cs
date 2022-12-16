using UniQuanda.Core.Domain.Entities.App;

namespace UniQuanda.Core.Application.Repositories
{
    public interface IUniversityRepository
    {
        /// <summary>
        ///     Gets all Universities where user is not present
        /// </summary>
        /// <param name="uid">User id</param>
        /// <param name="ct"></param>
        /// <returns></returns>
        public Task<IEnumerable<UniversityEntity>> GetUniversitiresWhereUserIsNotPresentAsync(int uid, CancellationToken ct);

        /// <summary>
        ///     Adds user to university
        /// </summary>
        /// <param name="userId">Id of user</param>
        /// <param name="universityId">Id of university</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        public Task<bool> AddUserToUniversityAsync(int userId, int universityId, CancellationToken ct);

        /// <summary>
        ///     Gets university with matching id
        /// </summary>
        /// <param name="universityId">Id of university</param>
        /// <param name="ct"></param>
        /// <returns>UniversityEntity if found, NULL otherwise</returns>
        public Task<UniversityEntity?> GetUniversityByIdAsync(int universityId, CancellationToken ct);
    }
}
