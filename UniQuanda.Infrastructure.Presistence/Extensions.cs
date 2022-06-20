using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UniQuanda.Infrastructure.Presistence.AppDb;
using UniQuanda.Infrastructure.Presistence.AuthDb;
using UniQuanda.Infrastructure.Presistence.Options;

namespace UniQuanda.Infrastructure.Presistence
{
    public static class Extensions
    {
        public static IServiceCollection AddInfrastructurePersistence(this IServiceCollection services, IConfiguration configuration)
        {
            var dbConnectionOptions = new DbConnectionOptions(configuration);
            services.AddSingleton(dbConnectionOptions);

            services.AddDbContext<AuthDbContext>(options => {
                options.UseNpgsql(dbConnectionOptions.AuthDb.ConnectionString);
            });

            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseNpgsql(dbConnectionOptions.AppDb.ConnectionString);
            });

            return services;
        }
    }
}
