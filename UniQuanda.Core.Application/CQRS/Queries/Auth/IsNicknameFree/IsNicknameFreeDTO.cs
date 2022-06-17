using System.ComponentModel.DataAnnotations;

namespace UniQuanda.Core.Application.CQRS.Queries.Auth.IsNicknameFree
{
    public class IsNicknameFreeRequestDTO
    {
        [Required]
        [MinLength(6)]
        [MaxLength(32)]
        public string Nickname { get; set; }
    }
}
