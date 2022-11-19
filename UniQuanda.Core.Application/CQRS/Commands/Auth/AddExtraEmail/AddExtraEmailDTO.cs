using System.ComponentModel.DataAnnotations;
using UniQuanda.Core.Application.Validators;
using UniQuanda.Core.Domain.Enums.Results;

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

public class AddExtraEmailResponseDTO
{
    public AppUserSecurityActionResultEnum ActionResult { get; set; }
}