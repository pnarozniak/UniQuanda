using Microsoft.Extensions.Configuration;

namespace UniQuanda.Infrastructure.Options;

public class SendGridOptions
{
    public SendGridOptions(IConfiguration configuration)
    {
        var section = configuration.GetSection("Email:SendGrid");
        ApiKey = section["ApiKey"];
        SenderEmail = section["SenderEmail"];
        SenderName = section["SenderName"];
        Templates = new TemplatesOptions(section.GetSection("Templates"));
    }

    public string ApiKey { get; set; }
    public string SenderEmail { get; set; }
    public string SenderName { get; set; }
    public TemplatesOptions Templates { get; set; }
}

public class TemplatesOptions
{
    public TemplatesOptions(IConfigurationSection section)
    {
        RegisterConfirmationId = section["RegisterConfirmationId"];
        AccountActionToConfirmId = section["AccountActionToConfirmId"];
        AccountActionFinishedId = section["AccountActionFinishedId"];
    }

    public string RegisterConfirmationId { get; set; }
    public string AccountActionToConfirmId { get; set; }
    public string AccountActionFinishedId { get; set; }

    public class RegisterConfirmationData
    {
        public string ConfirmationCode { get; set; }
        public string ConfirmationLink { get; set; }
        public string TermsAndConditionsLink { get; set; }
    }

    public class AccountActionToConfirmData
    {
        public string Title { get; set; }
        public string Subtitle { get; set; }
        public string ButtonLabel { get; set; }
        public string ButtonLink { get; set; }
        public string UserAgentBrowser { get; set; }
        public string UserAgentOs { get; set; }
        public string SupportContactLink { get; set; }
    }

    public class AccountActionFinishedData
    {
       public string Title { get; set; }
        public string Subtitle { get; set; }
        public string UserAgentBrowser { get; set; }
        public string UserAgentOs { get; set; }
        public string SupportContactLink { get; set; }
    }
}