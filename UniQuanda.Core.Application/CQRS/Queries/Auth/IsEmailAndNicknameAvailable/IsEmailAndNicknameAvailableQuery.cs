using MediatR;

namespace UniQuanda.Core.Application.CQRS.Queries.Auth.IsEmailAndNicknameAvailable
{
    public class IsEmailAndNicknameAvailableQuery : IRequest<IsEmailAndNicknameAvailableResponseDTO>
    {
        public string Email { get; set; }
        public string Nickname { get; set; }
        public IsEmailAndNicknameAvailableQuery(IsEmailAndNicknameAvailableRequestDTO request)
        {
            this.Email = request.Email;
            this.Nickname = request.Nickname;
        }
    }
}
