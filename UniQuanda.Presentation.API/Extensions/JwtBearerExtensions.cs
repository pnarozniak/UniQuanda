using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Security.Claims;
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
                options.TokenValidationParameters = accessTokenOptions.ValidationParameters;
            });

        return services;
    }

    /// <summary>
    ///     Get id of AppUser from token
    /// </summary>
    /// <param name="claimPrincipal">AppUser JWT Token</param>
    /// <returns>Id of AppUser if exists, otherwise null</returns>
    public static int? GetId(this ClaimsPrincipal claimPrincipal)
    {
        var idAppUser = claimPrincipal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var canParse = int.TryParse(idAppUser, out var parsedIdAppUser);
        if (!canParse)
            return null;
        return parsedIdAppUser;
    }
}