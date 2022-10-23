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
}