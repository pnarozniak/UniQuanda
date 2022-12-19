using UniQuanda.Core.Domain.Entities.App;
using UniQuanda.Core.Domain.Utils;

namespace UniQuanda.Core.Application.Repositories
{
    public interface IRoleRepository
    {
        /// <summary>
        ///     Gets all roles that are not expired
        /// </summary>
        /// <param name="userId">Id of user</param>
        /// <param name="ct"></param>
        /// <returns>List of all not expired roles that user has.</returns>
        public Task<IEnumerable<AppRoleEntity>> GetNotExpiredUserRolesAsync(int userId, CancellationToken ct);

        /// <summary>
        ///    Assignes app role to user
        /// </summary>
        /// <param name="userId">id of user</param>
        /// <param name="role">role to assign </param>
        /// <param name="validUntil">When role become invalid. If null, role is permanent</param>
        /// <param name="ct"></param>
        /// <returns>True if title is added, false otherwise</returns>
        public Task<bool> AssignAppRoleToUserAsync(int userId, AppRole role, DateTime? validUntil, CancellationToken ct);
    }
}
