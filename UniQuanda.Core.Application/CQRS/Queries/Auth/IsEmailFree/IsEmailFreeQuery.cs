using MediatR;

namespace UniQuanda.Core.Application.CQRS.Queries.Auth.IsEmailFree
{
    public class IsEmailFreeQuery : IRequest<bool>
    {
        public string Email { get; set; }
        public IsEmailFreeQuery(IsEmailFreeRequestDTO request)
        {
            this.Email = request.Email;
        }
    }
}
