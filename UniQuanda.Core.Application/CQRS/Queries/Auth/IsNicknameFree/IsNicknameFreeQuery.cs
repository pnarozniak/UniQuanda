using MediatR;

namespace UniQuanda.Core.Application.CQRS.Queries.Auth.IsNicknameFree
{
    public class IsNicknameFreeQuery : IRequest<bool>
    {
        public string Nickname { get; set; }
        public IsNicknameFreeQuery(IsNicknameFreeRequestDTO request)
        {
            Nickname = request.Nickname;
        }
    }
}
