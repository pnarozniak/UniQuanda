using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using UniQuanda.Infrastructure.Options;

namespace UniQuanda.Presentation.API.Extensions;

public static class JwtBearerExtensions
{
    public static IServiceCollection AddJwtBearerAuth(this IServiceCollection services, IConfiguration configuration)
    {
        var accessTokenOptions = new TokensOptions(configuration).AccessToken;
        services
            .AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = accessTokenOptions.ValidateIssuer,
                    ValidIssuer = accessTokenOptions.ValidIssuer,
                    ValidateAudience = accessTokenOptions.ValidateAudience,
                    ValidAudience = accessTokenOptions.ValidAudience,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(accessTokenOptions.SecretKey)),
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };
            });

        return services;
    }
}