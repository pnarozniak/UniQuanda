namespace UniQuanda.Core.Application.CQRS.Queries.Security.GetUserEmails;

public class GetUserEmailsReponseDTO
{
    public string MainEmail { get; set; }
    public IEnumerable<string> ExtraEmails { get; set; }
}
