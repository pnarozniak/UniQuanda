using UniQuanda.Core.Domain.Utils;

namespace UniQuanda.Core.Application.Services.Auth;

public interface IOAuthService
{
    /// <summary>
    ///     Fetches google id token from google oauth API, based od google oauth login confirmation code
    /// </summary>
    /// <returns>GoogleIdToken if operation was successful, otherwise Null</returns>
    Task<GoogleIdToken?> GetGoogleIdTokenAsync(string code);

    /// <summary>
    ///     Gets uniquanda client url, which handles google oauth login responses
    /// </summary>
    /// <returns>Uniquanda client oauth handler url</returns>
    string GetGoogleClientHandlerUrl();
}