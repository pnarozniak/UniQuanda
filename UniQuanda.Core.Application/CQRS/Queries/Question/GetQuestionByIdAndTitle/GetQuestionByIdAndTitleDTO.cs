using System.ComponentModel.DataAnnotations;

namespace UniQuanda.Core.Application.CQRS.Queries.Question.GetQuestionByIdAndTitle
{
    public class GetQuestionByIdAndTitleRequestDTO
    {
        [Required]
        public int QuestionId { get; set; }
        public string Title { get; set; }
    }

    public class GetQuestionByIdAndTitleResponseDTO
    {
        public int QuestionId { get; set; }
        public string Content { get; set; }
    }
}
