using System.ComponentModel.DataAnnotations;
using UniQuanda.Core.Application.Validators;

namespace UniQuanda.Core.Application.CQRS.Commands.Auth.AddExtraEmail;

public class AddExtraEmailRequestDTO
{
    [Required]
    [MaxLength(320)]
    [EmailAddress]
    public string NewExtraEmail { get; set; }

    [Required]
    [PasswordValidator]
    public string Password { get; set; }
}