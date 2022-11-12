using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace UniQuanda.Infrastructure.Options;

public class TokensOptions
{
    public TokensOptions(IConfiguration configuration)
    {
        var tokensSection = configuration.GetSection("Tokens");
        RefreshToken = new RefreshTokenOptions(tokensSection.GetSection("RefreshToken"));
        AccessToken = new AccessTokenOptions(tokensSection.GetSection("AccessToken"));
    }

    public RefreshTokenOptions RefreshToken { get; set; }
    public AccessTokenOptions AccessToken { get; set; }
}

public class RefreshTokenOptions
{
    public RefreshTokenOptions(IConfigurationSection section)
    {
        ValidityInMinutes = int.Parse(section["ValidityInMinutes"]);
    }

    public int ValidityInMinutes { get; set; }
}

public class AccessTokenOptions
{
    public AccessTokenOptions(IConfigurationSection section)
    {
        SecretKey = section["SecretKey"];
        ValidIssuer = section["ValidIssuer"];
        ValidAudience = section["ValidAudience"];
        ValidateIssuer = bool.Parse(section["ValidateIssuer"]);
        ValidateAudience = bool.Parse(section["ValidateAudience"]);
        ValidityInMinutes = int.Parse(section["ValidityInMinutes"]);
    }

    public AccessTokenOptions(string secretKey, string validIssuer, string validAudience, bool validateIssuer,
        bool validateAudience, int validityInMinutes)
    {
        SecretKey = secretKey;
        ValidIssuer = validIssuer;
        ValidAudience = validAudience;
        ValidateIssuer = validateIssuer;
        ValidateAudience = validateAudience;
        ValidityInMinutes = validityInMinutes;
    }

    public string SecretKey { get; set; }
    public string ValidIssuer { get; set; }
    public string ValidAudience { get; set; }
    public bool ValidateIssuer { get; set; }
    public bool ValidateAudience { get; set; }
    public int ValidityInMinutes { get; set; }

    public TokenValidationParameters ValidationParameters =>
        new TokenValidationParameters
        {
            ValidateIssuer = ValidateIssuer,
            ValidIssuer = ValidIssuer,
            ValidateAudience = ValidateAudience,
            ValidAudience = ValidAudience,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey)),
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero
        };
}