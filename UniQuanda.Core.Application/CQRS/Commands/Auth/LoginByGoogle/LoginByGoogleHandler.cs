using MediatR;
using UniQuanda.Core.Application.Repositories;
using UniQuanda.Core.Application.Services.Auth;
using UniQuanda.Core.Domain.Enums;

namespace UniQuanda.Core.Application.CQRS.Commands.Auth.LoginByGoogle;

public class LoginByGoogleHandler : IRequestHandler<LoginByGoogleCommand, string>
{
    private readonly IAuthRepository _authRepository;
    private readonly ITokensService _tokensService;
    private readonly IOAuthService _oAuthService;
    private readonly string _clientHandlerUrl;

    public LoginByGoogleHandler(
        IAuthRepository authRepository,
        ITokensService tokensService,
        IOAuthService oAuthService
    )
    {
        _authRepository = authRepository;
        _tokensService = tokensService;
        _oAuthService = oAuthService;
        _clientHandlerUrl = _oAuthService.GetGoogleClientHandlerUrl();
    }

    public async Task<string> Handle(LoginByGoogleCommand request, CancellationToken ct)
    {

        var idToken = await _oAuthService.GetGoogleIdTokenAsync(request.Code);
        if (idToken is null) return $"{_clientHandlerUrl}?error=500";

        var user = await _authRepository.GetUserByEmailAsync(idToken.Email, ct);
        if (user is null) {
            var isRegistered = await _authRepository.RegisterOAuthUserAsync(
                idToken.Id, idToken.Email, request.Code, OAuthProviderEnum.Google, ct);
            return _clientHandlerUrl + (isRegistered ? $"?confirmationCode={request.Code}" : "?error=409");
        }

        if (!user.IsOAuthUser) return $"{_clientHandlerUrl}?error=409";

        if (!user.IsOAuthRegisterCompleted) {
            await _authRepository.UpdateOAuthUserRegisterConfirmationCodeAsync(user.Id, request.Code, ct);
            return $"{_clientHandlerUrl}?confirmationCode={request.Code}";
        }

        return await GoogleLoginSuccess(user.Id, ct);
    }

    private async Task<string> GoogleLoginSuccess(int idUser, CancellationToken ct)
    {
        var (refreshTokenValue, refreshTokenExp) = _tokensService.GenerateRefreshToken();
        await _authRepository.UpdateUserRefreshTokenAsync(idUser, refreshTokenValue, refreshTokenExp, ct);

        var accessToken = _tokensService.GenerateAccessToken(idUser, true);
        return $"{_clientHandlerUrl}?accessToken={accessToken}";
    }
}