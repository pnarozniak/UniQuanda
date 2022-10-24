using System.ComponentModel.DataAnnotations;
using UniQuanda.Core.Application.Validators;

namespace UniQuanda.Core.Application.CQRS.Commands.Auth.UpdateUserMainEmail;

public class UpdateUserMainEmailRequestDTO
{
    [Required]
    [MaxLength(320)]
    [EmailAddress]
    public string NewMainEmail { get; set; }

    [Required]
    [PasswordValidator]
    public string Password { get; set; }
}


public enum UpdateUserMainEmailResponseDTO
{
    PasswordIsInvalid,
    EmailIsNotConnectedWithUser,
    UpdateError
}