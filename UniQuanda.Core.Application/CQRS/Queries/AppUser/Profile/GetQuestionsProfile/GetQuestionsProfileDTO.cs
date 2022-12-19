using System.ComponentModel.DataAnnotations;

namespace UniQuanda.Core.Application.CQRS.Queries.Profile.GetQuestionsProfile
{
    public class GetQuestionsProfileRequestDTO
    {
        /// <summary>
        /// Id of user
        /// </summary>
        [Required]
        public int UserId { get; set; }
        /// <summary>
        /// Page number
        /// </summary>
        [Required]
        public int Page { get; set; }
        /// <summary>
        /// Amount of questions on page
        /// </summary>
        [Required]
        [Range(1, 20)]
        public int PageSize { get; set; }
        /// <summary>
        /// Should response contain amount of questions in database
        /// </summary>
        [Required]
        public bool AddCount { get; set; }
    }

    public class GetQuestionsProfileResponseDTO
    {
        public IEnumerable<GetQuestionsProfileResponseDTOQuestion>? Questions { get; set; }
        public int? TotalCount { get; set; }
    }

    public class GetQuestionsProfileResponseDTOQuestion
    {
        public int Id { get; set; }
        public string Header { get; set; }
        public string Html { get; set; }
        public int Views { get; set; }
        public int Answers { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool HasCorrectAnswer { get; set; }
        public IEnumerable<string> TagNames { get; set; }
    }
}
