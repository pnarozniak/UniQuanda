using MediatR;
using UniQuanda.Core.Domain.Enums;

namespace UniQuanda.Core.Application.CQRS.Commands.Admin.Titles.AssignStatusToRequest
{
    public class AssignStatusToRequestCommand : IRequest<bool>
    {
        public AssignStatusToRequestCommand(AssignStatusToRequestDTORequest request)
        {
            this.Status = request.Status;
            this.RequestId = request.ReuqestId;
        }
        public TitleRequestStatusEnum Status { get; set; }
        public int RequestId { get; set; }
    }
}

