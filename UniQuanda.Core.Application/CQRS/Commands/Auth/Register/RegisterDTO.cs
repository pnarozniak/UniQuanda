using System.ComponentModel.DataAnnotations;
using UniQuanda.Core.Application.Validators;

namespace UniQuanda.Core.Application.CQRS.Commands.Auth.Register;

public class RegisterRequestDTO
{
    [Required]
    [MinLength(3)]
    [MaxLength(30)]
    public string Nickname { get; set; }

    [Required][PasswordValidator] public string Password { get; set; }

    [Required]
    [MaxLength(320)]
    [EmailAddress]
    public string Email { get; set; }

    [MaxLength(35)] public string? FirstName { get; set; }

    [MaxLength(51)] public string? LastName { get; set; }

    [DateTimeEarlierThanCurrentValidator] public DateTime? Birthdate { get; set; }

    [MaxLength(22)] public string? PhoneNumber { get; set; }

    [MaxLength(57)] public string? City { get; set; }
}