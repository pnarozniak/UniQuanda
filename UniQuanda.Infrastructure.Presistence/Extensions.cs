using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UniQuanda.Infrastructure.Presistence.AuthDb;
using UniQuanda.Infrastructure.Presistence.Options;

namespace UniQuanda.Infrastructure.Presistence
{
    public static class Extensions
    {
        public static IServiceCollection AddInfrastructurePersistence(this IServiceCollection services, IConfiguration configuration)
        {
            var authDbOptions = new AuthDbOptions(configuration);
            services.AddSingleton(authDbOptions);
            services.AddSingleton(new AuthDbDataExpirationOptions(configuration));

            services.AddDbContext<AuthDbContext>(options => {
                options.UseNpgsql(authDbOptions.ConnectionString);
            });

            return services;
        }
    }
}
