using UniQuanda.Infrastructure.Presistence.AppDb.Models;

namespace UniQuanda.Infrastructure.Presistence.AuthDb.Models
{
	public class Report
	{
		public Report()
		{
		}

        public int Id { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedAt { get; set; }

        public int ReportTypeId { get; set; }
        public virtual ReportType ReportTypeIdNavigation { get; set; }

        public int ReporterId { get; set; }
        public virtual AppUser ReporterIdNavigation { get; set; }

        public int? ReportedAnswerId { get; set; }
        public virtual Answer ReportedAnswerIdNavigation { get; set; }

        public int? ReportedQuestionId { get; set; }
        public virtual Question ReportedQuestionIdNavigation { get; set; }

        public int? ReportedUserId { get; set; }
        public virtual AppUser ReportedUserIdNavigation { get; set; }
    }
}

