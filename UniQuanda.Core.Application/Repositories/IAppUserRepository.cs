using UniQuanda.Core.Domain.Entities.App;

namespace UniQuanda.Core.Application.Repositories
{
    public interface IAppUserRepository
    {
        /// <summary>
        /// Returns user data needed to display profile
        /// </summary>
        /// <param name="uid">ID of profile to load</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>AppUserEntity with user data if profile exists, null otherwise</returns>
        public Task<AppUserEntity?> GetUserProfile(int uid, CancellationToken ct);
    }
}
