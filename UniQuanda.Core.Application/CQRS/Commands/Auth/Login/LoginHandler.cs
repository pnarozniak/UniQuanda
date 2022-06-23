using MediatR;
using UniQuanda.Core.Application.Repositories;
using UniQuanda.Core.Application.Services.Auth;

namespace UniQuanda.Core.Application.CQRS.Commands.Auth.Login;

public class LoginHandler : IRequestHandler<LoginCommand, LoginResponseDTO>
{
    private readonly IAuthRepository _authRepository;
    private readonly IPasswordsService _passwordsService;
    private readonly ITokensService _tokensService;

    public LoginHandler(
        IAuthRepository authRepository,
        IPasswordsService passwordsService,
        ITokensService tokensService)
    {
        _authRepository = authRepository;
        _passwordsService = passwordsService;
        _tokensService = tokensService;
    }

    public async Task<LoginResponseDTO> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var appUser = await _authRepository.GetUserByEmailAsync(request.Email);
        if (appUser is null || !_passwordsService.VerifyPassword(request.Password, appUser.HashedPassword))
            return new LoginResponseDTO
            {
                Status = LoginResponseDTO.LoginStatus.InvalidCredentials
            };

        if (!appUser.IsEmailConfirmed)
            return new LoginResponseDTO
            {
                Status = LoginResponseDTO.LoginStatus.EmailNotConfirmed
            };

        var accessToken = _tokensService.GenerateAccessToken(appUser);
        var (refreshToken, refreshTokenExp) = _tokensService.GenerateRefreshToken();
        await _authRepository.UpdateUserRefreshTokenAsync(appUser.Id, refreshToken, refreshTokenExp);

        return new LoginResponseDTO
        {
            Status = LoginResponseDTO.LoginStatus.Success,
            AccessToken = accessToken,
            RefreshToken = refreshToken,
            Nickname = appUser.Nickname,
            Avatar = appUser.OptionalInfo?.Avatar
        };
    }
}