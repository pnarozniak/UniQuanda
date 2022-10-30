using System.ComponentModel.DataAnnotations;
using UniQuanda.Core.Application.Validators;

namespace UniQuanda.Core.Application.CQRS.Commands.Auth.UpdateMainEmail;

public class UpdateMainEmailRequestDTO
{
    [MaxLength(320)]
    [EmailAddress]
    public string NewMainEmail { get; set; }

    public int? IdExtraEmail { get; set; }

    [Required]
    [PasswordValidator]
    public string Password { get; set; }
}