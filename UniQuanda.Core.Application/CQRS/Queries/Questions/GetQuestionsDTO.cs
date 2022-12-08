using System.ComponentModel.DataAnnotations;
using UniQuanda.Core.Domain.Enums;

namespace UniQuanda.Core.Application.CQRS.Queries.Questions
{
    public class GetQuestionsRequestDTO
    {
        [Required]
        public int Page { get; set; }
        [Required]
        [Range(1, 20)]
        public int PageSize { get; set; }
        [Required]
        public bool AddCount { get; set; }
        public IEnumerable<int>? Tags { get; set; }
        [Required]
        public OrderDirectionEnum OrderBy { get; set; }
        [Required]
        public QuestionSortingEnum SortBy { get; set; }
    }

    public class GetQuestionsResponseDTO
    {
        public int? Count { get; set; }
        public IEnumerable<GetQuestionsResponseDTOQuestion> Questions { get; set; }
    }

    public class GetQuestionsResponseDTOQuestion
    {
        public int Id { get; set; }
        public string Header { get; set; }
        public string Html { get; set; }
        public int Views { get; set; }
        public int AnswersCount { get; set; }
        public DateTime CreationDate { get; set; }
        public bool IsPopular { get; set; }
        public bool HasCorrectAnswer { get; set; }
        public GetQuestionsResponseDTOUser User { get; set; }
        public IEnumerable<string> TagNames { get; set; }
    }

    public class GetQuestionsResponseDTOUser
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ProfilePictureURL { get; set; }
    }

}
