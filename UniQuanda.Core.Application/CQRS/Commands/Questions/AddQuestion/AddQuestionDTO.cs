using System.ComponentModel.DataAnnotations;
using UniQuanda.Core.Application.Shared.Models;
using UniQuanda.Core.Application.Validators;
namespace UniQuanda.Core.Application.CQRS.Commands.Questions.AddQuestion
{
    public class AddQuestionRequestDTO: IContent
    {
        /// <summary>
        ///     Question title
        /// </summary>
        [Required]
        [StringLength(100, MinimumLength = 5)]
        public string Title { get; set; }

        /// <summary>
        ///     HTML text. Not formated, as is from request.
        /// </summary>
        [Required]
        [Sanitaze]
        public string RawText { get ; set ; }

        /// <summary>
        ///     Did user confirm, that similar question haven't been added.
        /// </summary>
        [BooleanRequiredTrue]
        public bool Confirmation { get; set; }

        /// <summary>
        ///     List of tags ids of new question
        /// </summary>
        [Required]
        [IEnumerableSizeValidation(1, 5)]
        public IEnumerable<int> TagIds { get; set; }
    }

    public class AddQuestionResponseDTO
    {      
        public int? QuestionId { get; set; }
    }
}
