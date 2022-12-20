using System.ComponentModel.DataAnnotations;
using UniQuanda.Core.Application.Validators;

namespace UniQuanda.Core.Application.CQRS.Commands.Test.GetAutomaticTest
{
    public class GetAutomaticTestRequestDTO
    {
        [Required]
        [IEnumerableSizeValidation(1, 5)]
        public IEnumerable<int> TagIds { get; set; }
    }

    public class GetAutomaticTestResponseDTO
    {
        public IEnumerable<AutomaticTestQuestionResponseDTO> Questions { get; set; }

        public class AutomaticTestQuestionResponseDTO
        {
            public int Id { get; set; }
            public DateTime CreatedAt { get; set; }
            public string Header { get; set; }
            public string HTML { get; set; }
            public AutomaticTestAnswerResponseDTO Answer { get; set; }
        }

        public class AutomaticTestAnswerResponseDTO
        {
            public int Id { get; set; }
            public string HTML { get; set; }
            public DateTime CreatedAt { get; set; }
            public int CommentsCount { get; set; }
        }
    }
}
