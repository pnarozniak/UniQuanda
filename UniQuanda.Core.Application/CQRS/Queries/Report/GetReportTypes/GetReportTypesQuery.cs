using MediatR;
using UniQuanda.Core.Application.CQRS.Queries.Auth.GetReportTypes;
using UniQuanda.Core.Domain.Enums;

namespace UniQuanda.Core.Application.CQRS.Queries.Report.GetReportTypes
{
    public class GetReportTypesQuery : IRequest<GetReportTypesResponseDTO>
    {
        public ReportCategoryEnum? ReportCategory { get; set; }
        public GetReportTypesQuery(GetReportTypesRequestDTO dto)
        {
            if (dto.Answer) ReportCategory = ReportCategoryEnum.ANSWER;
            if (dto.Question) ReportCategory = ReportCategoryEnum.QUESTION;
            if (dto.User) ReportCategory = ReportCategoryEnum.USER;
        }
    }
}