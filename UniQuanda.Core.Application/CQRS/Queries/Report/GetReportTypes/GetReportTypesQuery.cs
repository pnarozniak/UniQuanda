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
            if(dto.Answers) ReportCategory = ReportCategoryEnum.ANSWER;
            if(dto.Questions) ReportCategory = ReportCategoryEnum.QUESTION;
            if(dto.Users) ReportCategory = ReportCategoryEnum.USER;
        }
    }
}