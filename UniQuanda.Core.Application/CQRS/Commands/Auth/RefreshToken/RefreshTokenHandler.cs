using MediatR;
using UniQuanda.Core.Application.CQRS.Commands.Auth.RefreshToken;
using UniQuanda.Core.Application.Repositories;
using UniQuanda.Core.Application.Services.Auth;
using UniQuanda.Core.Domain.Utils;

namespace UniQuanda.Core.Application.CQRS.Commands.Auth.ConfirmRegister;

public class RefreshTokenHandler : IRequestHandler<RefreshTokenCommand, RefreshTokenResponseDTO?>
{
    private readonly IAuthRepository _authRepository;
    private readonly ITokensService _tokensService;
    private readonly IRoleRepository _roleRepository;

    public RefreshTokenHandler(
        IAuthRepository authRepository,
        ITokensService tokensService,
        IRoleRepository roleRepository
    )
    {
        _authRepository = authRepository;
        _tokensService = tokensService;
        _roleRepository = roleRepository;
    }

    public async Task<RefreshTokenResponseDTO?> Handle(RefreshTokenCommand request, CancellationToken ct)
    {
        var idUser = _tokensService.GetUserIdFromExpiredAccessToken(request.AccessToken);
        if (idUser is null) return null;

        var dbUser = await _authRepository.GetUserByIdAsync((int)idUser, ct);
        if (dbUser is null || dbUser.RefreshToken != request.RefreshToken) return null;

        var isTokenValid = dbUser.RefreshTokenExp > DateTime.UtcNow;
        if (!isTokenValid) return null;

        var authRoles = new List<AuthRole>() { };
        authRoles.Add(dbUser.IsOAuthUser ? new AuthRole() { Value = AuthRole.OAuthAccount } : new AuthRole() { Value = AuthRole.UniquandaAccount });
        var appRoles = await _roleRepository.GetNotExpiredUserRolesAsync(idUser ?? 0, ct);

        var accessToken = _tokensService.GenerateAccessToken(dbUser.Id, appRoles, authRoles);
        var (refreshToken, refreshTokenExp) = _tokensService.GenerateRefreshToken();
        await _authRepository.UpdateUserRefreshTokenAsync(dbUser.Id, refreshToken, refreshTokenExp, ct);

        return new RefreshTokenResponseDTO
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken
        };
    }
}