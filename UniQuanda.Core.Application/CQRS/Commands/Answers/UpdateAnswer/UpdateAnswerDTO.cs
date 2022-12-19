using System.ComponentModel.DataAnnotations;
using UniQuanda.Core.Application.Validators;

namespace UniQuanda.Core.Application.CQRS.Commands.Answers.UpdateAnswer;

public class UpdateAnswerRequestDTO
{
    /// <summary>
    ///     HTML text. Not formated, as is from request.
    /// </summary>
    [Required]
    [Sanitaze]
    public string RawText { get; set; }

    [Required]
    public int? IdAnswer { get; set; }
}