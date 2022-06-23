using System.ComponentModel.DataAnnotations;

namespace UniQuanda.Core.Application.CQRS.Queries.Auth.IsEmailAndNicknameAvailable;

public class IsEmailAndNicknameAvailableRequestDTO
{
    [Required]
    [EmailAddress]
    [MaxLength(320)]
    public string Email { get; set; }

    [Required]
    [MinLength(3)]
    [MaxLength(30)]
    public string Nickname { get; set; }
}

public class IsEmailAndNicknameAvailableResponseDTO
{
    public bool IsEmailAvailable { get; set; }
    public bool IsNicknameAvailable { get; set; }
}