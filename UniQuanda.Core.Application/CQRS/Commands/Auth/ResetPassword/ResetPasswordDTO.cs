using System.ComponentModel.DataAnnotations;
using UniQuanda.Core.Application.Validators;

namespace UniQuanda.Core.Application.CQRS.Commands.Auth.ResetPasword;

public class ResetPaswordDTO
{
		[Required]
    public string RecoveryToken { get; set; }

    [Required]
    [EmailAddress]
    [MaxLength(320)]
    public string Email { get; set; }

		[Required]
		[PasswordValidator]
		public string NewPassword { get; set; }
}