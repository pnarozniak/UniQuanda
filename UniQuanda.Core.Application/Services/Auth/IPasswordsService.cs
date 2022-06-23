namespace UniQuanda.Core.Application.Services.Auth;

public interface IPasswordsService
{
    /// <summary>
    ///     Hashes given password
    /// </summary>
    /// <param name="plainPassword">Password to hash</param>
    /// <returns>Password hash</returns>
    string HashPassword(string plainPassword);

    /// <summary>
    ///     Verifies that the given hash matches given plain password
    /// </summary>
    /// <param name="plainPassword">Plain|Raw password to compare</param>
    /// <param name="hashedPassword">Hashed password to compare</param>
    /// <returns>True if passwords match, otherwise False</returns>
    bool VerifyPassword(string plainPassword, string hashedPassword);
}