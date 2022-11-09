using UniQuanda.Core.Domain.ValueObjects;

namespace UniQuanda.Core.Domain.Entities.Auth;

public class UserEntity
{
    public int Id { get; set; }
    public string Nickname { get; set; }
    public string HashedPassword { get; set; }
    public bool IsEmailConfirmed { get; set; }
    public UserOptionalInfo OptionalInfo { get; set; }
    public IEnumerable<string> Emails { get; set; }
    public string? RefreshToken { get; set; }
    public DateTime? RefreshTokenExp { get; set; }
}