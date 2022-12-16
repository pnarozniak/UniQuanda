using System.ComponentModel.DataAnnotations;
using UniQuanda.Core.Domain.Enums;


namespace UniQuanda.Core.Application.CQRS.Queries.Profile.GetProfile
{
    public class GetProfileRequestDTO
    {
        [Required]
        public int UserId { get; set; }
    }

    public class GetProfileResponseDTO
    {
        public UserDataResponseDTO UserData { get; set; }
        public IEnumerable<AcademicTitleResponseDTO> AcademicTitles { get; set; }
        public IEnumerable<UniversityResponseDTO> Universities { get; set; }
        public HeaderStatisticsResponseDTO HeaderStatistics { get; set; }
        public IEnumerable<PointsInTagsResponseDTO> PointsInTags { get; set; }


        public class UserDataResponseDTO
        {
            public int Id { get; set; }
            public string Nickname { get; set; }
            public string? FirstName { get; set; }
            public string? LastName { get; set; }
            public string? Avatar { get; set; }
            public string? Banner { get; set; }
            public string? AboutText { get; set; }
            public string? Contact { get; set; }
            public string? City { get; set; }
            public DateTime? Birthdate { get; set; }
            public string? SemanticScholarProfile { get; set; }
            public bool HasPremium { get; set; }
        }
        public class UniversityResponseDTO
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Logo { get; set; }
            public int Order { get; set; }
        }

        public class AcademicTitleResponseDTO
        {
            public string Name { get; set; }
            public int Order { get; set; }
            public AcademicTitleEnum AcademicTitleType { get; set; }
        }

        public class HeaderStatisticsResponseDTO
        {
            public int Points { get; set; }
            public int Questions { get; set; }
            public int Answers { get; set; }
        }

        public class PointsInTagsResponseDTO
        {
            public string Name { get; set; }
            public int Points { get; set; }
            public int Order { get; set; }
        }
    }

}
