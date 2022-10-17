using UniQuanda.Core.Domain.Entities.App;
using UniQuanda.Core.Domain.Utils;

namespace UniQuanda.Core.Application.Repositories;

public interface IAppUserProfileRepository
{
    /// <summary>
    /// Returns user data needed to display profile
    /// </summary>
    /// <param name="uid">ID of profile to load</param>
    /// <param name="ct">Cancellation token</param>
    /// <returns>AppUserEntity with user data if profile exists, null otherwise</returns>
    public Task<AppUserEntity?> GetUserProfileAsync(int uid, CancellationToken ct);

    /// <summary>
    /// Returns avatar of user if exists
    /// </summary>
    /// <param name="uid">ID of user</param>
    /// <param name="ct">Cancellation token</param>
    /// <returns>String with url to avatar of user, null if user has no awatar</returns>
    public Task<string?> GetUserAvatarAsync(int uid, CancellationToken ct);

    /// <summary>
    ///     Get AppUser by id for settings profile
    /// </summary>
    /// <param name="idAppUser">Id by which appUser will be searched</param>
    /// <param name="ct">Operation cancellation token</param>
    /// <returns>AppUser if found, otherwise NULL</returns>
    Task<AppUserEntity?> GetAppUserByIdForProfileSettingsAsync(int idAppUser, CancellationToken ct);

    /// <summary>
    ///     Update AppUser information
    /// </summary>
    /// <param name="appUser">New data</param>
    /// <param name="ct">Operation cancellation token</param>
    /// <returns>If AppUser isn't found then IsSuccessful NULL, otherwise true/false based on success of update. If it is successful, returned is avatar url</returns>    
    Task<AppUserProfileUpdateResult> UpdateAppUserAsync(AppUserEntity appUser, CancellationToken ct);
}