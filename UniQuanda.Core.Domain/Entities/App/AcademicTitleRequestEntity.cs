using UniQuanda.Core.Domain.Enums;
using UniQuanda.Core.Domain.ValueObjects;

namespace UniQuanda.Core.Domain.Entities.App
{
    public class AcademicTitleRequestEntity
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? AdditionalInfo { get; set; }
        public Image Scan { get; set; }
        public TitleRequestStatusEnum Status { get; set; }
        public AppUserEntity User { get; set; }
        public AcademicTitleEntity Title { get; set; }

    }
}
