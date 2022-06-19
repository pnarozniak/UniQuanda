using System.ComponentModel.DataAnnotations;

namespace UniQuanda.Core.Application.CQRS.Commands.Auth.Login
{
    public class LoginRequestDTO
    {
        [Required]
        [EmailAddress]
        [MaxLength(320)]
        public string Email { get; set; }


        [Required]
        [RegularExpression("^.*[A-Z]+[a-z]+[0-9]+.*")]
        [MinLength(8)]
        [MaxLength(30)]
        public string Password { get; set; }
    }

    public class LoginResponseDTO
    {
        public enum LoginStatus
        {
            Success = 0, 
            InvalidCredentials = 1,
            EmailNotConfirmed = 2,
        }

        public LoginStatus Status { get; set; }
        public string? AccessToken { get; set; }
        public string? RefreshToken { get; set; }
        public string? Nickname { get; set; }
        public string? Avatar { get; set; }
    }
}
