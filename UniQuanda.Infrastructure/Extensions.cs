using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UniQuanda.Core.Application.Repositories;
using UniQuanda.Core.Application.Services;
using UniQuanda.Core.Application.Services.Auth;
using UniQuanda.Infrastructure.Options;
using UniQuanda.Infrastructure.Repositories;
using UniQuanda.Infrastructure.Services;
using UniQuanda.Infrastructure.Services.Auth;

namespace UniQuanda.Infrastructure
{
    public static class Extensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            // Options
            services.AddSingleton(new SendGridOptions(configuration));

            // Repositories
            services.AddScoped<IQuestionRepository, QuestionRepository>();
            services.AddScoped<IAuthRepository, AuthRepository>();
            
            // Services
            services.AddScoped<IPasswordsService, PasswordsService>();
            services.AddScoped<ITokensService, TokensService>();
            services.AddScoped<IEmailService, SendGridService>();

            return services;
        }
    }
}
