using MediatR;
using UniQuanda.Core.Application.CQRS.Commands.Auth.RefreshToken;
using UniQuanda.Core.Application.Repositories;
using UniQuanda.Core.Application.Services.Auth;

namespace UniQuanda.Core.Application.CQRS.Commands.Auth.ConfirmRegister;

public class RefreshTokenHandler : IRequestHandler<RefreshTokenCommand, RefreshTokenResponseDTO?>
{
    private readonly IAuthRepository _authRepository;
    private readonly ITokensService _tokensService;

    public RefreshTokenHandler(
        IAuthRepository authRepository,
        ITokensService tokensService
    )
    {
        _authRepository = authRepository;
        _tokensService = tokensService;
    }

    public async Task<RefreshTokenResponseDTO?> Handle(RefreshTokenCommand request, CancellationToken ct)
    {
        var idUser = _tokensService.GetUserIdFromExpiredAccessToken(request.AccessToken);
        if (idUser is null) return null;

        var dbUser = await _authRepository.GetUserByIdAsync((int)idUser, ct);
        if (dbUser is null || dbUser.RefreshToken != request.RefreshToken) return null;

        var isTokenValid = dbUser.RefreshTokenExp > DateTime.UtcNow;
        if (!isTokenValid) return null;

        var accessToken = _tokensService.GenerateAccessToken(dbUser.Id, dbUser.IsOAuthUser);
        var (refreshToken, refreshTokenExp) = _tokensService.GenerateRefreshToken();
        await _authRepository.UpdateUserRefreshTokenAsync(dbUser.Id, refreshToken, refreshTokenExp, ct);

        return new RefreshTokenResponseDTO
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken
        };
    }
}