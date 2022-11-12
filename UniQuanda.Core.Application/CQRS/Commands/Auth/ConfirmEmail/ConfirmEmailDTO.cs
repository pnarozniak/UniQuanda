using System.ComponentModel.DataAnnotations;

namespace UniQuanda.Core.Application.CQRS.Commands.Auth.ConfirmEmail;

public class ConfirmEmailRequestDTO
{
    [Required]
    [EmailAddress]
    [MaxLength(320)]
    public string Email { get; set; }

    [Required]
    public string ConfirmationCode { get; set; }
}
