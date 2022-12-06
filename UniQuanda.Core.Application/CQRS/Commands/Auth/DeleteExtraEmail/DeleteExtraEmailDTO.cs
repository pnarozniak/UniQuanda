using System.ComponentModel.DataAnnotations;
using UniQuanda.Core.Application.Validators;
using UniQuanda.Core.Domain.Enums.Results;

namespace UniQuanda.Core.Application.CQRS.Commands.Auth.DeleteExtraEmail;

public class DeleteExtraEmailRequestDTO
{
    [Required]
    public int? IdExtraEmail { get; set; }

    [Required]
    [PasswordValidator]
    public string Password { get; set; }
}

public class DeleteExtraEmailResponseDTO
{
    public AppUserSecurityActionResultEnum ActionResult { get; set; }
}