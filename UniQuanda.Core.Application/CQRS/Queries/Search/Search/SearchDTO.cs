using System.ComponentModel.DataAnnotations;


namespace UniQuanda.Core.Application.CQRS.Queries.Search.Search
{
    public class SearchRequestDTO
    {
        [Required]
        [MinLength(3)]
        public string SearchText { get; set; }
    }

    public class SearchResponseDTO
    {
        public IEnumerable<UserSearchResponseDTO> Users { get; set; }
        public IEnumerable<QuestionSearchResponseDTO> Questions { get; set; }
        public IEnumerable<UniversitySearchResponseDTO> Universities { get; set; }

        public class UserSearchResponseDTO
        {
            public int Id { get; set; }
            public string Nickname { get; set; }
        }

        public class QuestionSearchResponseDTO
        {
            public int Id { get; set; }
            public string Header { get; set; }
        }

        public class UniversitySearchResponseDTO
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }
    }

}
