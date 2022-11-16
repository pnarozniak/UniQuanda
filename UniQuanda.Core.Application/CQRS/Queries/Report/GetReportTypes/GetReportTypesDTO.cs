namespace UniQuanda.Core.Application.CQRS.Queries.Auth.GetReportTypes;

public class GetReportTypesRequestDTO
{
    public bool Question { get; set; }
    public bool Answer { get; set; }
    public bool User { get; set; }
}

public class GetReportTypesResponseDTO
{
    public IEnumerable<ReportTypeResponseDTO> Items { get; set; }
}

public class ReportTypeResponseDTO
{
    public int Id { get; set; }
    public string Name { get; set; }
}