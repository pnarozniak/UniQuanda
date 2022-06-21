using System.ComponentModel.DataAnnotations;

namespace UniQuanda.Core.Application.CQRS.Commands.Auth.ResendRegisterConfirmationCode
{
    public class ResendRegisterConfirmationCodeRequestDTO
    {
        [Required]
        [EmailAddress]
        [MaxLength(320)]
        public string Email { get; set; }
    }
}
