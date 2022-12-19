using System.ComponentModel.DataAnnotations;
using UniQuanda.Core.Application.Validators;

namespace UniQuanda.Core.Application.CQRS.Commands.Auth.ConfirmOAuthRegister
{
    public class ConfirmOAuthRegisterRequestDTO
    {
        [Required]
        public string ConfirmationCode { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(30)]
        public string Nickname { get; set; }
        [MaxLength(35)] public string? FirstName { get; set; }

        [MaxLength(51)] public string? LastName { get; set; }

        [DateTimeEarlierThanCurrentValidator] public DateTime? Birthdate { get; set; }

        [MaxLength(22)] public string? Contact { get; set; }

        [MaxLength(57)] public string? City { get; set; }
    }

    public class ConfirmOAuthRegisterResponseDTO
    {
        public string AccessToken { get; set; }
    }
}