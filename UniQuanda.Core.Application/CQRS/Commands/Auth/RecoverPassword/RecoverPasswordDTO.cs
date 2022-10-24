using System.ComponentModel.DataAnnotations;

namespace UniQuanda.Core.Application.CQRS.Commands.Auth.RecoverPassword;

public class RecoverPasswordDTO
{
    [Required]
    [EmailAddress]
    [MaxLength(320)]
    public string Email { get; set; }
}