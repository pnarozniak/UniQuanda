using UniQuanda.Core.Domain.Enums;

namespace UniQuanda.Core.Application.CQRS.Queries.AppUser.Settings.GetAllTitlesSettings
{
    public class GetAllTitlesSettingsResponseDTO
    {
        public int TitleId { get; set; }
        public string Name { get; set; }
        public AcademicTitleEnum Type { get; set; }
    }
}
