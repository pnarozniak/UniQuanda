using SendGrid;
using SendGrid.Helpers.Mail;
using UniQuanda.Core.Application.Services;
using UniQuanda.Infrastructure.Options;

namespace UniQuanda.Infrastructure.Services;

public class SendGridService : IEmailService
{
    private readonly SendGridOptions _options;

    public SendGridService(SendGridOptions options)
    {
        _options = options;
    }

    public async Task SendRegisterConfirmationEmailAsync(string to, string confirmationToken)
    {
        var emailMessage = $"<span>Your code is: <b>{confirmationToken}</b></span>";
        var emailSubject = "Confirm your email to finish registration";
        await SendEmailAsync(to, emailSubject, emailMessage);
    }

    private async Task SendEmailAsync(string email, string subject, string message)
    {
        var client = new SendGridClient(_options.ApiKey);
        var emailMessage = new SendGridMessage
        {
            From = new EmailAddress(_options.SenderEmail, _options.SenderName),
            Subject = subject,
            PlainTextContent = message,
            HtmlContent = message
        };
        emailMessage.AddTo(new EmailAddress(email));

        emailMessage.SetClickTracking(false, false);
        emailMessage.SetOpenTracking(false);
        emailMessage.SetGoogleAnalytics(false);
        emailMessage.SetSubscriptionTracking(false);

        await client.SendEmailAsync(emailMessage);
    }
}