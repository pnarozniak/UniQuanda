using UniQuanda.Core.Domain.Entities.Auth;

namespace UniQuanda.Core.Application.Services.Auth;

public interface ITokensService
{
    /// <summary>
    ///     Generates random 6 digit e-mail confirmation token
    /// </summary>
    /// <returns>Random 6 digit e-mail confirmation token</returns>
    string GenerateEmailConfirmationToken();

    /// <summary>
    ///     Generates random refresh token
    /// </summary>
    /// <returns>Returns tuple with refresh token and it's expiration</returns>
    Tuple<string, DateTime> GenerateRefreshToken();

    /// <summary>
    ///     Generates token for password recovery
    /// </summary>
    /// <returns>Password recovery token</returns>
    string GeneratePasswordRecoveryToken();

    /// <summary>
    ///     Generates access token for given user
    /// </summary>
    /// <returns>Access token as string</returns>
    string GenerateAccessToken(UserEntity user);

    /// <summary>
    ///     Validates access token, without its lifetime and parses its claims.
    /// </summary>
    /// <param name="accessToken">Access token as string</param>
    /// <returns>User id or null if validation fails</returns>
    int? GetUserIdFromExpiredAccessToken(string accessToken);
}