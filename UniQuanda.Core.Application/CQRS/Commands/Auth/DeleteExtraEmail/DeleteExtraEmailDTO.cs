using System.ComponentModel.DataAnnotations;
using UniQuanda.Core.Application.Validators;

namespace UniQuanda.Core.Application.CQRS.Commands.Auth.DeleteExtraEmail;

public class DeleteExtraEmailRequestDTO
{
    public int IdExtraEmail { get; set; }

    [Required]
    [PasswordValidator]
    public string Password { get; set; }
}
