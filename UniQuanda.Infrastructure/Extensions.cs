using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UniQuanda.Core.Application.Repositories;
using UniQuanda.Core.Application.Services;
using UniQuanda.Core.Application.Services.Auth;
using UniQuanda.Infrastructure.Options;
using UniQuanda.Infrastructure.Repositories;
using UniQuanda.Infrastructure.Services;
using UniQuanda.Infrastructure.Services.Auth;

namespace UniQuanda.Infrastructure;

public static class Extensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        // Options
        services.AddSingleton(new SendGridOptions(configuration));
        services.AddSingleton(new DataExpirationOptions(configuration));
        services.AddSingleton(new TokensOptions(configuration));

        // Repositories
        services.AddScoped<IAuthRepository, AuthRepository>();
        services.AddScoped<IAppUserProfileRepository, AppUserProfileRepository>();

        // Services
        services.AddScoped<IPasswordsService, PasswordsService>();
        services.AddScoped<ITokensService, TokensService>();
        services.AddScoped<IEmailService, SendGridService>();
        services.AddScoped<IExpirationService, ExpirationService>();

        return services;
    }
}