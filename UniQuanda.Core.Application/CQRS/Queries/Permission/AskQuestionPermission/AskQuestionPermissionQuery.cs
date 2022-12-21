using MediatR;
using UniQuanda.Core.Application.Shared.Models;

namespace UniQuanda.Core.Application.CQRS.Queries.Permission.AskQuestionPermission
{
    public class AskQuestionPermission : IRequest<LimitCheckResponseDTO>
    {
        public AskQuestionPermission(int UserId)
        {
            this.UserId = UserId;
        }
        public int UserId { get; set; }
    }
}
