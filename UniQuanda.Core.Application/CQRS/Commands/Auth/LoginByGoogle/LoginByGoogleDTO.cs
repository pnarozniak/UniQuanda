using System.ComponentModel.DataAnnotations;

namespace UniQuanda.Core.Application.CQRS.Commands.Auth.LoginByGoogle
{
    public class LoginByGoogleRequestDTO
    {
        [Required]
        public string Code { get; set; }
    }
}