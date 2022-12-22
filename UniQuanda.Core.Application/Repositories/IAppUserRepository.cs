using UniQuanda.Core.Domain.Entities.App;

namespace UniQuanda.Core.Application.Repositories;

public interface IAppUserRepository
{
    /// <summary>
    ///     Returns user data needed to display profile
    /// </summary>
    /// <param name="uid">ID of profile to load</param>
    /// <param name="ct">Cancellation token</param>
    /// <returns>AppUserEntity with user data if profile exists, null otherwise</returns>
    public Task<AppUserEntity?> GetUserProfileAsync(int uid, CancellationToken ct);

    /// <summary>
    ///     Returns avatar of user if exists
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
    /// <param name="isNewAvatar">Information if new avatar is added</param>
    /// <param name="isNewBanner">Information if new banner is added</param>
    /// <param name="ct">Operation cancellation token</param>
    /// <returns> Null when data not exists, otherwise is success of update</returns>
    Task<bool?> UpdateAppUserAsync(AppUserEntity appUser, bool isNewAvatar, bool isNewBanner, CancellationToken ct);

    /// <summary>
    ///     Checks if given nickname is currently used by any user
    /// </summary>
    /// <param name="uid">Id User which make request to API</param>
    /// <param name="nickname">Nickname to check</param>
    /// <param name="ct">Operation cancellation token</param>
    /// <returns>True if nickname is already in use, null when user is not exist, otherwise False</returns>
    Task<bool?> IsNicknameUsedAsync(int uid, string nickname, CancellationToken ct);

    /// <summary>
    ///     Returns if user has role premium
    /// </summary>
    /// <param name="idUser">Id user to check</param>
    /// <param name="ct">Operation cancellation token</param>
    /// <returns></returns>
    Task<bool> HasUserPremiumAsync(int idUser, CancellationToken ct);

    /// <summary>
    ///     Update user points in tags for like value
    /// </summary>
    /// <param name="idAnswer">Id answer</param>
    /// <param name="LikesIncreasedBy">Like increased by</param>
    Task UpdateAppUserPointsForLikeValueInTagsAsync(int idAnswer, int LikesIncreasedBy);

    /// <summary>
    ///     Update user points in tags for correct answer
    /// </summary>
    /// <param name="idAnswer">Id answer</param>
    /// <param name="idAuthorPrevCorrectAnswer">Id author previous correct answer</param>
    Task UpdateAppUserPointsForCorrectAnswerInTagsAsync(int idAnswer, int? idAuthorPrevCorrectAnswer);
}