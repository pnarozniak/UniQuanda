using UniQuanda.Core.Domain.Entities.Auth;

namespace UniQuanda.Core.Application.Repositories;

public interface ISecurityRepository
{
    /// <summary>
    ///     Gets all emails connected with user
    /// </summary>
    /// <param name="idUser">Id of User</param>
    /// <param name="ct">Operation cancellation token</param>
    /// <returns>User emails, if user not exists then NULL</returns>
    Task<UserEmailsEntity?> GetUserEmailsAsync(int idUser, CancellationToken ct);

    /// <summary>
    ///     Gets user hashed password by id
    /// </summary>
    /// <param name="idUser">Id user</param>
    /// <param name="ct">Operation cancellation token</param>
    /// <returns>HashedPassword if user exists, otherwise NULL</returns>
    Task<string?> GetUserHashedPasswordByIdAsync(int idUser, CancellationToken ct);

    /// <summary>
    ///     Update main email for user
    /// </summary>
    /// <param name="idUser">Id of user which main email will be updated </param>
    /// <param name="newMainEmail">New main email</param>
    /// <param name="ct">Operation cancellation token</param>
    /// <returns>True if update is succesful, NULL when user not exist, false when update is not succesful</returns>
    Task<bool?> UpdateUserMainEmailAsync(int idUser, string newMainEmail, CancellationToken ct);

    /// <summary>
    ///     Check if email is connected with User
    /// </summary>
    /// <param name="idUser">Id of user to check email with</param>
    /// <param name="email">Email to check</param>
    /// <param name="ct">Operation cancellation token</param>
    /// <returns>True if email is connected with user, otherwise false</returns>
    Task<bool> IsEmailConnectedWithUserAsync(int idUser, string email, CancellationToken ct);

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
}