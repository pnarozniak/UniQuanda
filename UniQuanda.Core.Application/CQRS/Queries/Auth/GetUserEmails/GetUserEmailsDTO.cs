using UniQuanda.Core.Domain.ValueObjects;

namespace UniQuanda.Core.Application.CQRS.Queries.Auth.GetUserEmails;

public class GetUserEmailsReponseDTO
{
    public UserEmailValue MainEmail { get; set; }
    public IEnumerable<UserEmailValue> ExtraEmails { get; set; }
}
