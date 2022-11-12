namespace UniQuanda.Core.Domain.ValueObjects;

public class UserEmailToConfirmValue : UserEmailValue
{
    public bool IsMainEmail { get; set; }
}
