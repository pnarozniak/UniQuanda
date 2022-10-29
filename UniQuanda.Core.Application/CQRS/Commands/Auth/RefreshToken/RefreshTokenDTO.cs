using System.ComponentModel.DataAnnotations;

namespace UniQuanda.Core.Application.CQRS.Commands.Auth.RefreshToken;

public class RefreshTokenRequestDTO
{
    [Required]
    public string AccessToken { get; set; }
    [Required]
    public string RefreshToken { get; set; }
}

public class RefreshTokenResponseDTO
{
    public string AccessToken { get; set; }
    public string RefreshToken { get; set; }
}