using System.ComponentModel.DataAnnotations;

namespace UniQuanda.Core.Application.CQRS.Queries.Auth.IsEmailFree
{
    public class IsEmailFreeRequestDTO
    {
        [Required]
        [EmailAddress]
        [MaxLength(320)]
        public string Email { get; set; }
    }
}
