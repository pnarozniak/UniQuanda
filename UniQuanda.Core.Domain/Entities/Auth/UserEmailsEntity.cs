namespace UniQuanda.Core.Domain.Entities.Auth;

public class UserEmailsEntity
{
    public string MainEmail { get; set; }
    public IEnumerable<string> ExtraEmails { get; set; }
}
