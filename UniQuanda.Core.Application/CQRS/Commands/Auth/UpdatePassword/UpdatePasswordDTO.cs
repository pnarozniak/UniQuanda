using System.ComponentModel.DataAnnotations;
using UniQuanda.Core.Application.Validators;
using UniQuanda.Core.Domain.Enums.Results;

namespace UniQuanda.Core.Application.CQRS.Commands.Auth.UpdatePassword;

public class UpdatePasswordRequestDTO
{
    [Required]
    [PasswordValidator]
    public string NewPassword { get; set; }

    [Required]
    [PasswordValidator]
    public string OldPassword { get; set; }
}

public class UpdatePasswordResponseDTO
{
    public AppUserSecurityActionResultEnum ActionResult { get; set; }
}