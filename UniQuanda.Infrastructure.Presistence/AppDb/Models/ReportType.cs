using UniQuanda.Core.Domain.Enums;

namespace UniQuanda.Infrastructure.Presistence.AuthDb.Models
{
	public class ReportType
	{
		public ReportType() {}

		public int Id { get; set; }
		public string Name { get; set; }
		public ReportCategoryEnum  ReportCategory { get; set; }

		public virtual ICollection<Report> Reports { get; set; }
	}
}
