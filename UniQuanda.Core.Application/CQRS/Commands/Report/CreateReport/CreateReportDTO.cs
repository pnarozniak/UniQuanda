using System.ComponentModel.DataAnnotations;

namespace UniQuanda.Core.Application.CQRS.Queries.Auth.CreateReport;

public class CreateReportRequestDTO
{
		[Required]
		public int? ReportedEntityId { get; set; }

		[Required]
		public int? ReportTypeId { get; set; }

		[MaxLength(300)]
		public string? Description { get; set; }
}
