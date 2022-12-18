using System.ComponentModel.DataAnnotations;
using UniQuanda.Core.Application.Shared.Models;
using UniQuanda.Core.Application.Validators;

namespace UniQuanda.Core.Application.CQRS.Commands.Answers.AddAnswer;

public class AddAnswerRequestDTO : IContent
{
    /// <summary>
    ///     HTML text. Not formated, as is from request.
    /// </summary>
    [Required]
    [Sanitaze]
    public string RawText { get; set; }

    [Required]
    public int? IdQuestion { get; set; }

    public int? ParentAnswerId { get; set; }
}