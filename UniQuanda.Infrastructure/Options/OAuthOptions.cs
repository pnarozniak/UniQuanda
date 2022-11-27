using Microsoft.Extensions.Configuration;

namespace UniQuanda.Infrastructure.Options;

public class OAuthOptions
{
    public OAuthOptions(IConfiguration configuration)
    {
        var section = configuration.GetSection("OAuth");
        GoogleOAuth = new GoogleOAuthOptions(section.GetSection("Google"));
    }

    public GoogleOAuthOptions GoogleOAuth { get; set; }
}

public class GoogleOAuthOptions
{
		public GoogleOAuthOptions(IConfigurationSection section)
    {
        TokenUrl = section["TokenUrl"];
        ClientId = section["ClientId"];
				ClientSecret = section["ClientSecret"];
        RedirectUri = section["RedirectUri"];
    }

    public string TokenUrl { get; set; }
    public string RedirectUri { get; set; }
		public string ClientId { get; set; }
		public string ClientSecret { get; set; }
}