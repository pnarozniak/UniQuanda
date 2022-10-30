using UniQuanda.Core.Domain.ValueObjects;

namespace UniQuanda.Core.Domain.Entities.Auth;

public class UserSecurityEntity
{
    public string Nickname { get; set; }
    public string HashedPassword { get; set; }
    public ICollection<UserEmailSecurity> Emails { get; set; }
}
