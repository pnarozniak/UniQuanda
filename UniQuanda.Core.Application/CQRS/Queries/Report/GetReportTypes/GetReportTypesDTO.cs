namespace UniQuanda.Core.Application.CQRS.Queries.Auth.GetReportTypes;

public class GetReportTypesRequestDTO
{
		public bool Questions { get; set; }
		public bool Answers { get; set; }
		public bool Users { get; set; }
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