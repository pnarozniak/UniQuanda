using MediatR;
using UniQuanda.Core.Application.CQRS.Queries.Auth.CreateReport;

namespace UniQuanda.Core.Application.CQRS.Commands.Report.CreateReport
{
    public class CreateReportCommand : IRequest<bool>
    {
        public int ReporterId { get; set; }
        public int ReportedEntityId { get; set; }
        public int ReportedTypeId { get; set; }
        public string? Description { get; set; }

        public CreateReportCommand(CreateReportRequestDTO dto, int reporterId)
        {
            ReporterId = reporterId;
            ReportedEntityId = dto.ReportedEntityId.Value;
            ReportedTypeId = dto.ReportTypeId.Value;
            Description = dto.Description;
        }
    }
}