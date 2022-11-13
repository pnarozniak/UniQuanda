using MediatR;
using UniQuanda.Core.Application.CQRS.Queries.Auth.GetReportTypes;
using UniQuanda.Core.Application.Repositories;
using UniQuanda.Core.Domain.Enums;

namespace UniQuanda.Core.Application.CQRS.Queries.Report.GetReportTypes
{
    public class GetReportTypesHandler : IRequestHandler<GetReportTypesQuery, GetReportTypesResponseDTO?>
    {
        private readonly IReportRepository _reportRepository;
        public GetReportTypesHandler(IReportRepository reportRepository)
        {
            _reportRepository = reportRepository;
        }

        public async Task<GetReportTypesResponseDTO?> Handle(GetReportTypesQuery request, CancellationToken ct)
        {
            if (request.ReportCategory is null) return null;

            var reportTypes = await this._reportRepository.GetReportTypesAsync((ReportCategoryEnum)request.ReportCategory, ct);
            return new GetReportTypesResponseDTO()
            {
                Items = reportTypes.Select(rt => new ReportTypeResponseDTO
                {
                    Id = rt.Id,
                    Name = rt.Name
                })
            };
        }
    }
}