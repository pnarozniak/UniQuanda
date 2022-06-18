using System.ComponentModel.DataAnnotations;

namespace UniQuanda.Core.Application.CQRS.Commands.Auth.Register
{
    public class RegisterRequestDTO
    {
        [Required]
        public string Nickname { get; set; }
        
        [Required]
        public string Password { get; set; }

        [Required]
        [MaxLength(320)]
        [EmailAddress]
        public string Email { get; set; }
        
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime? Birthdate { get; set; }
        public string? PhoneNumber { get; set; }
        public string? City { get; set; }
    }
}
