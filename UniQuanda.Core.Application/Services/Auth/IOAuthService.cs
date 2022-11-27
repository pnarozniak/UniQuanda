using UniQuanda.Core.Domain.Utils;

namespace UniQuanda.Core.Application.Services.Auth;

public interface IOAuthService
{
    Task<GoogleIdToken?> GetGoogleIdTokenAsync(string code);
    string GetGoogleClientHandlerUrl();
}