using System.ComponentModel.DataAnnotations;

namespace UniQuanda.Core.Application.CQRS.Commands.Auth.ConfirmRegister
{
    public class ConfirmRegisterRequestDTO
    {
        [Required]
        [EmailAddress]
        [MaxLength(320)]
        public string Email { get; set; }

        [Required]
        [MaxLength(6)]
        [MinLength(6)]
        public string ConfirmationCode { get; set; }
    }
}
