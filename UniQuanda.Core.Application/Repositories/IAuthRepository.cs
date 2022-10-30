﻿using UniQuanda.Core.Domain.Entities.Auth;
using UniQuanda.Core.Domain.Enums;

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
    ///     Gets registered user by id
    /// </summary>
    /// <param name="idUser">Id by which user will be searched</param>
    /// <param name="ct">Operation cancellation token</param>
    /// <returns>User if found, otherwise NULL</returns>
    Task<UserSecurityEntity?> GetUserByIdAsync(int idUser, CancellationToken ct);

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
    ///     Checks if email is available
    /// </summary>
    /// <param name="idUser">If null check all rows, otherwise query skip id user </param>
    /// <param name="email">Email to check</param>
    /// <param name="ct">Operation cancellation token</param>
    /// <returns>True if email is available, otherwise false</returns>
    Task<bool> IsEmailAvailableAsync(int? idUser, string email, CancellationToken ct);

    /// <summary>
    ///     Gets all emails connected with user
    /// </summary>
    /// <param name="idUser">Id of user to get emails</param>
    /// <param name="ct">Operation cancellation token</param>
    /// <returns>True if User emails exists, otherwise NULL</returns>
    Task<UserEmailsEntity?> GetUserEmailsAsync(int idUser, CancellationToken ct);

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
    Task<bool?> UpdateUserMainEmailByExtraEmailAsync(int idUser, int idExtraEmail, CancellationToken ct);

    /// <summary>
    ///     Check if email is connected with User as extra email and returns id
    /// </summary>
    /// <param name="idUser">Id of user to check email connection</param>
    /// <param name="email">Email to check</param>
    /// <param name="ct">Operation cancellation token</param>
    /// <returns>Email id if email is connected with user, otherwise NULL</returns>
    Task<int?> GetExtraEmailIdAsync(int idUser, string email, CancellationToken ct);

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
    /// <returns>True if succesful, NULL when extra email not exists or given email is main, false when delete is not succesful</returns>
    Task<bool?> DeleteExtraEmailAsync(int idUser, int idExtraEmail, CancellationToken ct);

    /// <summary>
    ///     Creates UserActionConfirmation of given action type for given user
    /// </summary>
    /// <param name="idUser">Id of user connected with action</param>
    /// <param name="actionType">Confirmation action type</param>
    /// <param name="confirmationToken">Confirmation token</param>
    /// <param name="existsUntil">Confirmation token expiration and a timestamp when record should be deleted from db</param>
    /// <param name="ct">Operation cancellation token</param>
    /// <returns>
    ///     True if user confirmation action has been successfully added to db, NULL if user was not found, otherwise
    ///     False
    /// </returns>
    Task<bool?> CreateUserActionToConfirmAsync(int idUser, UserActionToConfirmEnum actionType, string confirmationToken,
        DateTime existsUntil, CancellationToken ct);

    /// <summary>
    ///     Gets user action confirmation by confirmation action type and token
    /// </summary>
    /// <param name="actionType">Confirmation action type</param>
    /// <param name="confirmationToken">Confirmation token value</param>
    /// <param name="ct">Operation cancellation token</param>
    /// <returns>User action to confirm entity if confirmation action has been found, otherwise NULL</returns>
    Task<UserActionToConfirmEntity?> GetUserActionToConfirmAsync(UserActionToConfirmEnum actionType,
        string confirmationToken, CancellationToken ct);

    /// <summary>
    ///     Resets user password, removes user refreshToken and deletes password recovery action
    /// </summary>
    /// <param name="idUser">Id of user reseting password</param>
    /// <param name="idRecoveryAction">Id of user password recovery action to confirm</param>
    /// <param name="newHashedPassword">User new hashed password</param>
    /// <param name="ct">Operation cancellation token</param>
    /// <returns>True if password has been changed and password recovery action has been deleted, otherwise False</returns>
    Task<bool> ResetUserPasswordAsync(int idUser, int idRecoveryAction, string newHashedPassword, CancellationToken ct);
}