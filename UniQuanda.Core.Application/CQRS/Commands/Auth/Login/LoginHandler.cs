using MediatR;
using UniQuanda.Core.Application.Repositories;
using UniQuanda.Core.Application.Services.Auth;
using UniQuanda.Core.Domain.Utils;

namespace UniQuanda.Core.Application.CQRS.Commands.Auth.Login;

public class LoginHandler : IRequestHandler<LoginCommand, LoginResponseDTO>
{
    private readonly IAppUserRepository _appUserRepository;
    private readonly IAuthRepository _authRepository;
    private readonly IPasswordsService _passwordsService;
    private readonly ITokensService _tokensService;
    private readonly IRoleRepository _roleRepository;

    public LoginHandler(
        IAuthRepository authRepository,
        IPasswordsService passwordsService,
        ITokensService tokensService,
        IAppUserRepository appUserRepository,
        IRoleRepository roleRepository)
    {
        _authRepository = authRepository;
        _passwordsService = passwordsService;
        _tokensService = tokensService;
        _appUserRepository = appUserRepository;
        _roleRepository = roleRepository;
    }

    public async Task<LoginResponseDTO> Handle(LoginCommand request, CancellationToken ct)
    {
        var appUser = await _authRepository.GetUserByEmailAsync(request.Email, ct);
        if (appUser is null || appUser.IsOAuthUser || !_passwordsService.VerifyPassword(request.Password, appUser.HashedPassword))
        {
            return new LoginResponseDTO
            {
                Status = LoginResponseDTO.LoginStatus.InvalidCredentials
            };
        }

        if (!appUser.IsEmailConfirmed)
            return new LoginResponseDTO
            {
                Status = LoginResponseDTO.LoginStatus.EmailNotConfirmed
            };
        var authRoles = new List<AuthRole>()
        {
            new AuthRole() { Value = AuthRole.UniquandaAccount }
        };
        var appRoles = await _roleRepository.GetNotExpiredUserRolesAsync(appUser.Id, ct);
        var accessToken = _tokensService.GenerateAccessToken(appUser.Id, appRoles, authRoles);
        var (refreshToken, refreshTokenExp) = _tokensService.GenerateRefreshToken();
        await _authRepository.UpdateUserRefreshTokenAsync(appUser.Id, refreshToken, refreshTokenExp, ct);
        var avatar = await _appUserRepository.GetUserAvatarAsync(appUser.Id, ct);
        return new LoginResponseDTO
        {
            Status = LoginResponseDTO.LoginStatus.Success,
            AccessToken = accessToken,
            RefreshToken = refreshToken,
            Nickname = appUser.Nickname,
            Avatar = avatar
        };
    }
}