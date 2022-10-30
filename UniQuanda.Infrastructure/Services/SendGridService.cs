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

    public async Task SendInformationAboutAddNewExtraEmailAsync(string to, string newExtraEmail)
    {
        var emailSubject = "Dodatkowy e-mail został dodany do konta";
        var emailMessage = $"<span>Do Twojego konta został przypisany nowy e-mail dodatkowy: <b>{newExtraEmail}</b>.</span>";
        await SendEmailAsync(to, emailSubject, emailMessage);
    }

    public async Task SendInformationAboutDeleteExtraEmailAsync(string to, string extraEmail)
    {
        var emailSubject = "Dodatkowy e-mail został usunięty z konta";
        var emailMessage = $"<span>Z Twojego konta został usunięty dodatkowy e-mail: <b>{extraEmail}</b>.</span>";
        await SendEmailAsync(to, emailSubject, emailMessage);
    }

    public async Task SendInformationAboutUpdateMainEmailAsync(string to, string newMainEmail)
    {
        var emailSubject = "Twój główny e-mail został zmieniony";
        var emailMessage = $"<span>Główny e-mail powiązany z Twoim kontem został zmieniony. Teraz głównym e-mailem twojego konta jest: <b>{newMainEmail}</b>.</span>";
        await SendEmailAsync(to, emailSubject, emailMessage);
    }

    public async Task SendInformationAboutUpdatePasswordAsync(string to, string nickName)
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
}