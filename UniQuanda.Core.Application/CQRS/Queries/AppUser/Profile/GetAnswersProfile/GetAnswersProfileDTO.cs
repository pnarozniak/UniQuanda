using System.ComponentModel.DataAnnotations;

namespace UniQuanda.Core.Application.CQRS.Queries.Profile.GetAnswersProfile
{
    public class GetAnswersProfileRequestDTO
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
        /// Should response contain amount of answers in database
        /// </summary>
        [Required]
        public bool AddCount { get; set; }
    }

    public class GetAnswersProfileResponseDTO
    {
        public IEnumerable<GetAnswersProfileResponseDTOAnswer> Answers { get; set; }
        public int? TotalCount { get; set; }
    }

    public class GetAnswersProfileResponseDTOAnswer
    {
        public int Id { get; set; }
        public int? ParentId { get; set; }
        public int QuestionId { get; set; }
        public int Page { get; set; }
        public string Header { get; set; }
        public string Html { get; set; }
        public int Likes { get; set; }
        public bool IsCorrect { get; set; }
        public DateTime CreatedAt { get; set; }
        public IEnumerable<string> TagNames { get; set; }
    }


}
