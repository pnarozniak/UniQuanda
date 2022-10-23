using SendGrid;
using SendGrid.Helpers.Mail;
using UniQuanda.Core.Application.Services;
using UniQuanda.Infrastructure.Options;

namespace UniQuanda.Infrastructure.Services;

// TODO - QUAN-142
public class SendGridService : IEmailService
{
    private readonly SendGridOptions _options;
    private readonly UniQuandaClientOptions _uniQuandaClientOptions;

    public SendGridService(SendGridOptions options, UniQuandaClientOptions uniQuandaClientOptions)
    {
        _options = options;
        _uniQuandaClientOptions = uniQuandaClientOptions;
    }

    public async Task SendRegisterConfirmationEmailAsync(string to, string confirmationToken)
    {
        var emailMessage = $"<span>Your code is: <b>{confirmationToken}</b></span>";
        var emailSubject = "Confirm your email to finish registration";
        await SendEmailAsync(to, emailSubject, emailMessage);
    }

    public async Task SendPasswordRecoveryEmailAsync(string to, string recoveryToken)
    {
        var url = $"{_uniQuandaClientOptions.Url}/public/password-recovery/reset-password?email={to}&recoveryToken={recoveryToken}";
        var emailMessage =
            $"<span>Click following link, to reset your password: <a href=\"{url}\">Reset password</a></span>";
        var emailSubject = "Password recovery";
        await SendEmailAsync(to, emailSubject, emailMessage);
    }

    public async Task SendPasswordHasBeenChangedEmailAsync(string to)
    {
        var emailMessage = "<span>Your password has been changed</span>";
        var emailSubject = "Password has been changed";
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