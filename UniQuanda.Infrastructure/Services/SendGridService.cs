using SendGrid;
using SendGrid.Helpers.Mail;
using UniQuanda.Core.Application.Services;
using UniQuanda.Core.Domain.Utils;
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
        var templateId = _options.Templates.RegisterConfirmationId;
        var templateData = new TemplatesOptions.RegisterConfirmationData {
            ConfirmationCode = confirmationToken,
            ConfirmationLink = $"{_uniQuandaClientOptions.Url}/public/confirm-registration?email={to}&code={confirmationToken}",
            TermsAndConditionsLink =  $"{_uniQuandaClientOptions.Url}/public/terms-and-conditions",
        };

        await SendEmailAsync(to, templateId, templateData);
    }

    public async Task SendPasswordRecoveryEmailAsync(string to, string recoveryToken, UserAgentInfo userAgentInfo)
    {
        var templateId = _options.Templates.AccountActionToConfirmId;
        var templateData = new TemplatesOptions.AccountActionToConfirmData {
            Title = "Ustaw nowe hasło",
            Subtitle = "Otrzymaliśmy prośbę o zmianę Twojego hasła. Możesz to zrobić, klikając w poniższy  przycisk:",
            ButtonLabel = "Zmień hasło",
            ButtonLink = $"{_uniQuandaClientOptions.Url}/public/reset-password?email={to}&recoveryToken={recoveryToken}",
            UserAgentBrowser = userAgentInfo.Browser ?? "???",
            UserAgentOs = userAgentInfo.Os ?? "???",
            SupportContactLink = $"{_uniQuandaClientOptions.Url}/public/support"
        };

        await SendEmailAsync(to, templateId, templateData);
    }

    public async Task SendEmailAboutUpdatedPasswordAsync(string to, string nickname, UserAgentInfo userAgentInfo)
    {
        var templateId = _options.Templates.AccountActionFinishedId;
        var templateData = new TemplatesOptions.AccountActionFinishedData {
            Title = "Hasło zostało zmienione",
            Subtitle = $"Hasło powiązane z twoim kontem <strong>\"{nickname}\"</strong> zostało zmienione.",
            UserAgentBrowser = userAgentInfo.Browser ?? "???",
            UserAgentOs = userAgentInfo.Os ?? "???",
            SupportContactLink = $"{_uniQuandaClientOptions.Url}/public/support"
        };

        await SendEmailAsync(to, templateId, templateData);
    }

    public async Task SendConfirmationEmailToAddNewExtraEmailAsync(string to, string nickname, string token, UserAgentInfo userAgentInfo) 
    {
        var templateId = _options.Templates.AccountActionToConfirmId;
        var templateData = new TemplatesOptions.AccountActionToConfirmData {
            Title = "Dodaj dodatkowy adres e-mail",
            Subtitle = $"Otrzymaliśmy prośbę o dodanie tego adresu e-mail jako dodatkowego do konta: <strong>\"{nickname}\"</strong>. Możesz to zrobić, klikając w poniższy  przycisk:",
            ButtonLabel = "Dodaj dodatkowy e-mail",
            ButtonLink = $"{_uniQuandaClientOptions.Url}/public/confirm-email?email={to}&token={token}",
            UserAgentBrowser = userAgentInfo.Browser ?? "???",
            UserAgentOs = userAgentInfo.Os ?? "???",
            SupportContactLink = $"{_uniQuandaClientOptions.Url}/public/support"
        };

        await SendEmailAsync(to, templateId, templateData);
    }

    public async Task SendEmailAboutAddedNewExtraEmailAsync(string to, string newExtraEmail, UserAgentInfo userAgentInfo)
    {
        var templateId = _options.Templates.AccountActionFinishedId;
        var templateData = new TemplatesOptions.AccountActionFinishedData {
            Title = "Dodatkowy e-mail został dodany do konta",
            Subtitle = $"Do twojego konta został przypisany nowy dodatkowy e-mail: <strong>\"{newExtraEmail}\"</strong>.",
            UserAgentBrowser = userAgentInfo.Browser ?? "???",
            UserAgentOs = userAgentInfo.Os ?? "???",
            SupportContactLink = $"{_uniQuandaClientOptions.Url}/public/support"
        };

        await SendEmailAsync(to, templateId, templateData);
    }

    public async Task SendEmailAboutDeletedExtraEmailAsync(string to, string extraEmail, UserAgentInfo userAgentInfo)
    {
        var templateId = _options.Templates.AccountActionFinishedId;
        var templateData = new TemplatesOptions.AccountActionFinishedData {
            Title = "Dodatkowy e-mail został usunięty",
            Subtitle = $"Z Twojego konta został usunięty dodatkowy e-mail: <strong>\"{extraEmail}\"</strong>.",
            UserAgentBrowser = userAgentInfo.Browser ?? "???",
            UserAgentOs = userAgentInfo.Os ?? "???",
            SupportContactLink = $"{_uniQuandaClientOptions.Url}/public/support"
        };

        await SendEmailAsync(to, templateId, templateData);
    }

    public async Task SendConfirmationEmailToUpdateMainEmailAsync(string to, string nickname, string token, UserAgentInfo userAgentInfo) 
    {
        var templateId = _options.Templates.AccountActionToConfirmId;
        var templateData = new TemplatesOptions.AccountActionToConfirmData {
            Title = "Zmień główny adres e-mail",
            Subtitle = $"Otrzymaliśmy prośbę o zmianę twojego głównego adresu e-mail powiązanego z kontem: <strong>\"{nickname}\"</strong>. Możesz to zrobić, klikając w poniższy  przycisk:",
            ButtonLabel = "Zmień głowy e-mail",
            ButtonLink = $"{_uniQuandaClientOptions.Url}/public/confirm-email?email={to}&token={token}",
            UserAgentBrowser = userAgentInfo.Browser ?? "???",
            UserAgentOs = userAgentInfo.Os ?? "???",
            SupportContactLink = $"{_uniQuandaClientOptions.Url}/public/support"
        };

        await SendEmailAsync(to, templateId, templateData);
    }
    
    public async Task SendEmailAboutUpdatedMainEmailAsync(string to, string newMainEmail, UserAgentInfo userAgentInfo)
    {
        var templateId = _options.Templates.AccountActionFinishedId;
        var templateData = new TemplatesOptions.AccountActionFinishedData {
            Title = "Twój główny e-mail został zmieniony",
            Subtitle = $"Główny e-mail powiązany z Twoim kontem został zmieniony. Teraz głównym e-mailem twojego konta jest: <strong>\"{newMainEmail}\"</strong>.",
            UserAgentBrowser = userAgentInfo.Browser ?? "???",
            UserAgentOs = userAgentInfo.Os ?? "???",
            SupportContactLink = $"{_uniQuandaClientOptions.Url}/public/support"
        };

        await SendEmailAsync(to, templateId, templateData);
    }

    private async Task SendEmailAsync(string email, string templateId, object templateData)
    {
        var client = new SendGridClient(_options.ApiKey);
        var emailMessage = new SendGridMessage
        {
            From = new EmailAddress(_options.SenderEmail, _options.SenderName),
        };
        emailMessage.AddTo(new EmailAddress(email));

        emailMessage.SetTemplateId(templateId);
        emailMessage.SetTemplateData(templateData);

        emailMessage.SetClickTracking(false, false);
        emailMessage.SetOpenTracking(false);
        emailMessage.SetGoogleAnalytics(false);
        emailMessage.SetSubscriptionTracking(false);

        await client.SendEmailAsync(emailMessage);
    }
}