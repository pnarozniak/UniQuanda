using MediatR;

namespace UniQuanda.Core.Application.CQRS.Queries.Auth.IsEmailAndNicknameAvailable;

public class IsEmailAndNicknameAvailableQuery : IRequest<IsEmailAndNicknameAvailableResponseDTO>
{
    public IsEmailAndNicknameAvailableQuery(IsEmailAndNicknameAvailableRequestDTO request)
    {
        Email = request.Email;
        Nickname = request.Nickname;
    }

    public string Email { get; set; }
    public string Nickname { get; set; }
}