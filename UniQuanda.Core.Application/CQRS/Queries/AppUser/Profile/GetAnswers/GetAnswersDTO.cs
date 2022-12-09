using System.ComponentModel.DataAnnotations;

namespace UniQuanda.Core.Application.CQRS.Queries.Profile.GetAnswers
{
    public class GetAnswersRequestDTO
    {
        [Required]
        public int UserId { get; set; }
        [Required]
        public int Page { get; set; }
        [Required]
        [Range(1, 20)]
        public int PageSize { get; set; }
        [Required]
        public bool AddCount { get; set; }
    }

    public class GetAnswersResponseDTO
    {
        public IEnumerable<GetAnswersResponseDTOAnswer>? Answers { get; set; }
        public int? TotalCount { get; set; }
    }

    public class GetAnswersResponseDTOAnswer
    {
        public int AnswerId { get; set; }
        public string Header { get; set; }
        public string Html { get; set; }
        public int Likes { get; set; }
        public bool IsCorrect { get; set; }
        public DateTime CreatedAt { get; set; }
        public IEnumerable<string> TagNames { get; set; }
    }

    public class GetAnswersResponseDTOTag
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }


}
