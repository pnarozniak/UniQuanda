using UniQuanda.Core.Domain.Entities;

namespace UniQuanda.Core.Application.Repositories
{
    public interface IAuthRepository
    {
        /// <summary>
        /// Checks if given e-mail is currently being used by any user
        /// </summary>
        /// <param name="email">E-mail to check</param>
        /// <returns>True if e-mail is already in use, otherwise False</returns>
        Task<bool> IsEmailUsedAsync(string email);

        /// <summary>
        /// Checks if given nickname is currently being used by any user
        /// </summary>
        /// <param name="nickname">Nickname to check</param>
        /// <returns>True if nickname is already in use, otherwise False</returns>
        Task<bool> IsNicknameUsedAsync(string nickname);

        /// <summary>
        /// Adds new user to database
        /// </summary>
        /// <param name="newUser">User to add</param>
        /// <returns>True if user has been successfully added to db, otherwise False</returns>
        Task<bool> RegisterNewUserAsync(NewUser newUser);
    }
}
