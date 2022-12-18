using System.ComponentModel.DataAnnotations;
using UniQuanda.Core.Domain.Enums;

namespace UniQuanda.Core.Application.CQRS.Queries.AppUser.Settings.GetTitlesSettings
{
    public class GetTitlesSettingsRequestDTO
    {
        [Required]
        public int UserId { get; set; }
    }

    public class GetTitlesSettingsResponseDTO
    {
        public int TitleId { get; set; }
        public AcademicTitleEnum Type { get; set; }
        public string Name { get; set; }
        public int Order { get; set; }
    }

}
