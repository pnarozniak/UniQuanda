using Microsoft.Extensions.Configuration;

namespace UniQuanda.Infrastructure.Options
{
    public class TokensOptions
    {
        public TokensOptions(IConfiguration configuration)
        {
            var tokensSection = configuration.GetSection("Tokens");
            this.RefreshToken = new RefreshTokenOptions(tokensSection.GetSection("RefreshToken"));
            this.AccessToken = new AccessTokenOptions(tokensSection.GetSection("AccessToken"));
        }

        public RefreshTokenOptions RefreshToken { get; set; }
        public AccessTokenOptions AccessToken { get; set; }
    }

    public class RefreshTokenOptions
    {
        public RefreshTokenOptions(IConfigurationSection section)
        {
            this.ValidityInMinutes = int.Parse(section["ValidityInMinutes"]);
        }
        
        public int ValidityInMinutes { get; set; }
    }
    
    public class AccessTokenOptions
    {
        public AccessTokenOptions(IConfigurationSection section)
        {
            this.SecretKey = section["SecretKey"];
            this.ValidIssuer = section["ValidIssuer"];
            this.ValidAudience = section["ValidAudience"];
            this.ValidateIssuer = bool.Parse(section["ValidateIssuer"]);
            this.ValidateAudience = bool.Parse(section["ValidateAudience"]);
            this.ValidityInMinutes = int.Parse(section["ValidityInMinutes"]);
        }

        public string SecretKey { get; set; }
        public string ValidIssuer { get; set; }
        public string ValidAudience { get; set; }
        public bool ValidateIssuer { get; set; }
        public bool ValidateAudience { get; set; }
        public int ValidityInMinutes { get; set; }
    }
}
