using UniQuanda.Core.Domain.Entities.App;
using UniQuanda.Core.Domain.Enums;
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

        /// <summary>
        ///     Returns user usages of permission with maximum avilable amount. And refresh period
        /// </summary>
        /// <param name="userId">id of user</param>
        /// <param name="permission">name of permission. example: ask-question</param>
        /// <param name="ct"></param>
        /// <returns>
        ///     <para>If user has role where roles that has no limit to use permision, result will be (null,null,null) </para>
        ///     <para>If user has not permission to execute given permission, result will be (0,0,null) </para>
        ///     <para>First value, is how many times user can execute permission in closestClearInterval </para>
        ///     <para>Second value is how many times during closestClearInterval user used permission </para>
        ///     <para>Third values is when usages of permission will be cleared </para>
        /// </returns>
        public Task<(int? maxAmount, int? usedAmount, DurationEnum? closestClearInterval)> GetExecutesOfPermissionByUserAsync(int userId, string permission, CancellationToken ct);

        /// <summary>
        ///     Adds execution of permission to user. <para>It doesn't chceck if user can execute permission</para>
        /// </summary>
        /// <param name="userId">Id of permission</param>
        /// <param name="permission">name of permission. Example: ask-question</param>
        /// <param name="ct"></param>
        /// <returns>Is usage added to user (is success)</returns>
        public Task<bool> AddExecutionOfPermissionToUserAsync(int userId, string permission, CancellationToken ct);
    }
}
