using MediatR;

namespace UniQuanda.Core.Application.CQRS.Commands.Auth.RefreshToken;

public class RefreshTokenCommand : IRequest<RefreshTokenResponseDTO?>
{
    public string AccessToken { get; set; }
    public string RefreshToken { get; set; }

    public RefreshTokenCommand(RefreshTokenRequestDTO request)
    {
        AccessToken = request.AccessToken;
        RefreshToken = request.RefreshToken;
    }
}