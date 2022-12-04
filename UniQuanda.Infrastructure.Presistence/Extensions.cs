using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using UniQuanda.Infrastructure.Presistence.AppDb;
using UniQuanda.Infrastructure.Presistence.AuthDb;
using UniQuanda.Infrastructure.Presistence.AuthDb.Jobs;
using UniQuanda.Infrastructure.Presistence.Options;

namespace UniQuanda.Infrastructure.Presistence;

public static class Extensions
{
    public static IServiceCollection AddInfrastructurePersistence(this IServiceCollection services,
        IConfiguration configuration)
    {
        var dbConnectionOptions = new DbConnectionOptions(configuration);
        services.AddSingleton(dbConnectionOptions);
        
        var quartzJobsSchedulesOptions = new QuartzJobsSchedulesOptions(configuration);
        services.AddSingleton(quartzJobsSchedulesOptions);

        services.AddDbContext<AuthDbContext>(options =>
        {
            options.UseNpgsql(dbConnectionOptions.AuthDb.ConnectionString);
        });

        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseNpgsql(dbConnectionOptions.AppDb.ConnectionString);
        });

        services.AddQuartz(q => 
        {
            q.UseMicrosoftDependencyInjectionJobFactory();
            var clearTempUsersJob = new JobKey("ClearTempUsersJob");
            q.AddJob<ClearTempUsersJob>(opts => opts.WithIdentity(clearTempUsersJob));
            q.AddTrigger(opts => opts
                .ForJob(clearTempUsersJob)
                .WithIdentity("ClearTempUsersJob-Trigger")
                .WithCronSchedule(quartzJobsSchedulesOptions.AuthDb.ClearTempUsers)
            );

            var clearUserActionsToConfirmJob = new JobKey("ClearUsersActionsToConfirmJob");
            q.AddJob<ClearUsersActionsToConfirmJob>(opts => opts.WithIdentity(clearUserActionsToConfirmJob));
            q.AddTrigger(opts => opts
                .ForJob(clearUserActionsToConfirmJob)
                .WithIdentity("ClearUsersActionsToConfirmJob-Trigger")
                .WithCronSchedule(quartzJobsSchedulesOptions.AuthDb.ClearUserActionsToConfirm)
            );

            var clearRefreshTokensJob = new JobKey("ClearRefreshTokensJob");
            q.AddJob<ClearRefreshTokensJob>(opts => opts.WithIdentity(clearRefreshTokensJob));
            q.AddTrigger(opts => opts
                .ForJob(clearRefreshTokensJob)
                .WithIdentity("ClearRefreshTokensJob-Trigger")
                .WithCronSchedule(quartzJobsSchedulesOptions.AuthDb.ClearRefreshTokens)
            );
        });
        services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);

        return services;
    }
}