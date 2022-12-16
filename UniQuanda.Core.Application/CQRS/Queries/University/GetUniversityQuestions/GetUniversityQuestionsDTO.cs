using System.ComponentModel.DataAnnotations;

namespace UniQuanda.Core.Application.CQRS.Queries.University.GetUniversityQuestions
{
    public class GetUniversityQuestionsRequestDTO
    {
        /// <summary>
        ///     Unique University identificator
        /// </summary>
        [Required]
        public int UniversityId { get; set; }
        /// <summary>
        ///     Page to load
        /// </summary>
        [Required]
        public int Page { get; set; }
        /// <summary>
        ///     How many items should response have
        /// </summary>
        [Required]
        [Range(1,20)]
        public int PageSize { get; set; }
        /// <summary>
        ///     Should response contain total size of questions asked by 
        /// </summary>
        [Required]
        public bool AddCount { get; set; }
    }

    public class GetUniversityQuestionsResponseDTO
    {
        public IEnumerable<GetUniversityQuestionsResponseDTOQuestion> Questions { get; set; }
        public int? TotalCount { get; set; }
    }

    public class GetUniversityQuestionsResponseDTOQuestion
    {
        public int Id { get; set; }
        public string Header { get; set; }
        public string Html { get; set; }
        public int Views { get; set; }
        public int AnswersCount { get; set; }
        public DateTime CreationDate { get; set; }
        public bool IsPopular { get; set; }
        public bool HasCorrectAnswer { get; set; }
        public IEnumerable<string> TagNames { get; set; }
        public GetUniversityQuestionsResponseDTOUser User { get; set; }

    }
    public class GetUniversityQuestionsResponseDTOUser
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ProfilePictureURL { get; set; }
    }
}
