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

    /// <summary>
    ///     Gets all emails connected with user
    /// </summary>
    /// <param name="idUser">Id of user to get emails</param>
    /// <param name="ct">Operation cancellation token</param>
    /// <returns>True if User emails exists, otherwise NULL</returns>
    Task<UserEmailsEntity?> GetUserEmailsAsync(int idUser, CancellationToken ct);

    /// <summary>
    ///     Gets user hashed password by id
    /// </summary>
    /// <param name="idUser">Id of user to get hashed password</param>
    /// <param name="ct">Operation cancellation token</param>
    /// <returns>HashedPassword if user exists, otherwise NULL</returns>
    Task<string?> GetUserHashedPasswordByIdAsync(int idUser, CancellationToken ct);

    /// <summary>
    ///     Update main email for user
    /// </summary>
    /// <param name="idUser">Id of user which main email will be updated</param>
    /// <param name="newMainEmail">New main email</param>
    /// <param name="ct">Operation cancellation token</param>
    /// <returns>True if update is succesful, NULL when user not exist, false when update is not succesful</returns>
    Task<bool?> UpdateUserMainEmailAsync(int idUser, string newMainEmail, CancellationToken ct);

    /// <summary>
    ///     Update user main email with existing extra email
    /// </summary>
    /// <param name="idUser">Id of user which main email will be updated</param>
    /// <param name="idExtraEmail">Id of extra email which will be main</param>
    /// <param name="ct">Operation cancellation token</param>
    /// <returns>True if update is succesful, NULL when extra or main email not exist, false when update is not succesful</returns>
    Task<bool?> UpdateUserMainEmailByExtraEmail(int idUser, int idExtraEmail, CancellationToken ct);

    /// <summary>
    ///     Check if email is connected with User as extra email and returns id
    /// </summary>
    /// <param name="idUser">Id of user to check email connection</param>
    /// <param name="email">Email to check</param>
    /// <param name="ct">Operation cancellation token</param>
    /// <returns>Email id if email is connected with user, otherwise NULL</returns>
    Task<int?> GetExtraEmailIdAsync(int idUser, string email, CancellationToken ct);

    /// <summary>
    ///     Checks if email is available
    /// </summary>
    /// <param name="email">Email to check</param>
    /// <param name="ct">Operation cancellation token</param>
    /// <returns>True if email is available, otherwise false</returns>
    Task<bool> IsEmailAvailableAsync(string email, CancellationToken ct);

    /// <summary>
    ///     Add extra email for User
    /// </summary>
    /// <param name="idUser">Id of User to add extra email</param>
    /// <param name="newExtraEmail">New extra email</param>
    /// <param name="ct">Operation cancellation token</param>
    /// <returns>True if add is succesful, NULL when user has 3 extra emails, false when update is not succesful</returns>
    Task<bool?> AddExtraEmailAsync(int idUser, string newExtraEmail, CancellationToken ct);

    /// <summary>
    ///     Overides user old hashed password
    /// </summary>
    /// <param name="idUser">Id of User to update</param>
    /// <param name="newHashedPassword">New hashed password</param>
    /// <param name="ct">Operation cancellation token</param>
    /// <returns>True if succesful, NULL when user not exists, false when update is not succesful</returns>
    Task<bool?> UpdateUserPasswordAsync(int idUser, string newHashedPassword, CancellationToken ct);

    /// <summary>
    ///     Delete extra email of user
    /// </summary>
    /// <param name="idUser">Id of user to delete extra email</param>
    /// <param name="idExtraEmail">Extra email id</param>
    /// <param name="ct">Operation cancellation token</param>
    /// <returns>True if succesful, NULL when extra email not exists, false when delete is not succesful</returns>
    Task<bool?> DeleteExtraEmailAsync(int idUser, int idExtraEmail, CancellationToken ct);
}