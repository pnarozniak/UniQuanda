using UniQuanda.Core.Domain.ValueObjects;

namespace UniQuanda.Core.Domain.Entities.Auth;

public class UserEmailsEntity
{
    public UserEmailValue MainEmail { get; set; }
    public IEnumerable<UserEmailValue> ExtraEmails { get; set; }
}
