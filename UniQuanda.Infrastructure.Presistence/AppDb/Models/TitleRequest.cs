using UniQuanda.Core.Domain.Enums;

namespace UniQuanda.Infrastructure.Presistence.AppDb.Models
{
    public class TitleRequest
    {
        public int Id { get; set; }
        public TitleRequestStatusEnum TitleRequestStatus { get; set; }
        public DateTime CreatedAt { get; set; }
        public string AdditionalInfo { get; set; }
        public int AppUserId { get; set; }
        public int AcademicTitleId { get; set; }
        public int ScanId { get; set; }
        public virtual AppUser AppIdNavigationUser { get; set; }
        public virtual AcademicTitle AcademicTitleIdNavigation { get; set; }
        public virtual Image ScanIdNavigation { get; set; }
    }
}
