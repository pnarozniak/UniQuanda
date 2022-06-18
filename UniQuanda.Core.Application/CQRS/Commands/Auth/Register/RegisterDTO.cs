using System.ComponentModel.DataAnnotations;

namespace UniQuanda.Core.Application.CQRS.Commands.Auth.Register
{
    public class RegisterRequestDTO
    {
        [Required]
        [MinLength(6)]
        [MaxLength(30)]
        public string Nickname { get; set; }
        
        [Required]
        [RegularExpression("^.*[A-Z]+[a-z]+[0-9]+.*")]
        [MinLength(8)]
        [MaxLength(30)]
        public string Password { get; set; }

        [Required]
        [MaxLength(320)]
        [EmailAddress]
        public string Email { get; set; }

        [MaxLength(30)]
        public string? FirstName { get; set; }

        [MaxLength(30)]
        public string? LastName { get; set; }

        public DateTime? Birthdate { get; set; }

        [MaxLength(9)]
        public string? PhoneNumber { get; set; }

        [MaxLength(30)]
        public string? City { get; set; }
    }
}
