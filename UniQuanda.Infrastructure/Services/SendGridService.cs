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
        var url = $"{_uniQuandaClientOptions.Url}/public/reset-password?email={to}&recoveryToken={recoveryToken}";
        var emailMessage =
            $"<span>Click following link, to reset your password: <a href=\"{url}\">Reset password</a></span>";
        var emailSubject = "Password recovery";
        await SendEmailAsync(to, emailSubject, emailMessage);
    }

    public async Task SendEmailAboutUpdatedPasswordAsync(string to)
    {
        var emailMessage = "<span>Your password has been changed</span>";
        var emailSubject = "Password has been changed";
        await SendEmailAsync(to, emailSubject, emailMessage);
    }

    public async Task SendEmailAboutAddedNewExtraEmailAsync(string to, string newExtraEmail)
    {
        var emailSubject = "Dodatkowy e-mail został dodany do konta";
        var emailMessage = $"<span>Do Twojego konta został przypisany nowy e-mail dodatkowy: <b>{newExtraEmail}</b>.</span>";
        await SendEmailAsync(to, emailSubject, emailMessage);
    }

    public async Task SendEmailAboutDeletedExtraEmailAsync(string to, string extraEmail)
    {
        var emailSubject = "Dodatkowy e-mail został usunięty z konta";
        var emailMessage = $"<span>Z Twojego konta został usunięty dodatkowy e-mail: <b>{extraEmail}</b>.</span>";
        await SendEmailAsync(to, emailSubject, emailMessage);
    }

    public async Task SendEmailAboutUpdatedMainEmailAsync(string to, string newMainEmail)
    {
        var emailSubject = "Twój główny e-mail został zmieniony";
        var emailMessage = $"<span>Główny e-mail powiązany z Twoim kontem został zmieniony. Teraz głównym e-mailem twojego konta jest: <b>{newMainEmail}</b>.</span>";
        await SendEmailAsync(to, emailSubject, emailMessage);
    }

    public async Task SendEmailAboutUpdatedPasswordAsync(string to, string nickName)
    {
        var emailSubject = "Twoje hasło zostało zmienione";
        var emailMessage = $"<span>Hasło powiązane z Twoim kontem <b>{nickName}</b> zostało zmienione.</span>";
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

    public async Task SendEmailWithEmailConfirmationLinkAsync(string to, string token)
    {
        var url = $"{_uniQuandaClientOptions.Url}/public/confirm-email?email={to}&token={token}";
        var emailMessage =
            $"<span>Kliknij w poniższy link by potwierdzić e-mail: <a href=\"{url}\">Potwiedź e-mail</a></span>";
        var emailSubject = "Potwierdzenie e-mail'a";
        await SendEmailAsync(to, emailSubject, emailMessage);
    }
}