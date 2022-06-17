using Microsoft.Extensions.Configuration;

namespace UniQuanda.Infrastructure.Options
{
    public class SendGridOptions
    {
        public SendGridOptions(IConfiguration configuration)
        {
            var section = configuration.GetSection("Email:SendGrid");
            ApiKey = section["ApiKey"];
            SenderEmail = section["SenderEmail"];
            SenderName = section["SenderName"];
        }
        
        public string ApiKey { get; set; }
        public string SenderEmail { get; set; }
        public string SenderName { get; set; }
    }
}
