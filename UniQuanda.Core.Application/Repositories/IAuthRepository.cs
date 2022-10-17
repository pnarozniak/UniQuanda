using UniQuanda.Core.Domain.Entities.Auth;

namespace UniQuanda.Core.Application.Repositories;

public interface IAuthRepository
{
    /// <summary>
    ///     Checks if given e-mail is currently used by any user
    /// </summary>
    /// <param name="email">E-mail to check</param>
    /// <param name="ct">Operation cancellation token</param>
    /// <returns>True if e-mail is already in use, otherwise False</returns>
    Task<bool> IsEmailUsedAsync(string email, CancellationToken ct);

    /// <summary>
    ///     Checks if given nickname is currently used by any user
    /// </summary>
    /// <param name="nickname">Nickname to check</param>
    /// <param name="ct">Operation cancellation token</param>
    /// <returns>True if nickname is already in use, otherwise False</returns>
    Task<bool> IsNicknameUsedAsync(string nickname, CancellationToken ct);

    /// <summary>
    ///     Adds new user to database
    /// </summary>
    /// <param name="newUser">User to add</param>
    /// <param name="ct">Operation cancellation token</param>
    /// <returns>True if user has been successfully added to db, otherwise False</returns>
    Task<bool> RegisterNewUserAsync(NewUserEntity newUser, CancellationToken ct);

    /// <summary>
    ///     Gets registered user by e-mail address
    /// </summary>
    /// <param name="email">Email by which user will be searched</param>
    /// <param name="ct">Operation cancellation token</param>
    /// <returns>User if found, otherwise NULL</returns>
    Task<UserEntity?> GetUserByEmailAsync(string email, CancellationToken ct);

    /// <summary>
    ///     Updates user refresh token
    /// </summary>
    /// <param name="idUser">Id of user to update</param>
    /// <param name="refreshToken">Refresh token to be updated</param>
    /// <param name="refreshTokenExp">Date and time of refresh token expiration</param>
    /// <param name="ct">Operation cancellation token</param>
    /// <returns>True if refresh token was updated, NULL if user was not found, otherwise False</returns>
    Task<bool?> UpdateUserRefreshTokenAsync(int idUser, string refreshToken, DateTime refreshTokenExp,
        CancellationToken ct);

    /// <summary>
    ///     Confirms user registration by given confirmation code and e-mail
    /// </summary>
    /// <param name="email">E-mail to confirm</param>
    /// <param name="confirmationCode">E-mail confirmation code</param>
    /// <param name="ct">Operation cancellation token</param>
    /// <returns>True if user registration has been confirmed successfully, otherwise False</returns>
    Task<bool> ConfirmUserRegistrationAsync(string email, string confirmationCode, CancellationToken ct);

    /// <summary>
    ///     Updates TempUser e-mail confirmation code
    /// </summary>
    /// <param name="email">E-mail of TempUser to update</param>
    /// <param name="confirmationCode">E-mail confirmation code to be updated</param>
    /// <param name="ct">Operation cancellation token</param>
    /// <returns>True if confirmation code has been updated successfully, NULL if TempUser was not found, otherwise False</returns>
    Task<bool?> UpdateTempUserEmailConfirmationCodeAsync(string email, string confirmationCode, CancellationToken ct);
}