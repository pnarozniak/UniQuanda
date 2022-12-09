using System.ComponentModel.DataAnnotations;

namespace UniQuanda.Core.Application.CQRS.Queries.Profile.GetQuestions
{
    public class GetQuestionsRequestDTO
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

    public class GetQuestionsResponseDTO
    {
        public IEnumerable<GetQuestionsResponseDTOQuestion>? Questions { get; set; }
        public int? TotalCount { get; set; }
    }

    public class GetQuestionsResponseDTOQuestion
    {
        public int Id { get; set; }
        public string Header { get; set; }
        public string Html { get; set; }
        public int Views { get; set; }
        public int Answers { get; set; }
        public DateTime CreatedAt { get; set; }
        public IEnumerable<string> TagNames { get; set; }
    }
}
