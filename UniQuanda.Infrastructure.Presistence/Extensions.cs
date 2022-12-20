using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using UniQuanda.Infrastructure.Presistence.AppDb;
using UniQuanda.Infrastructure.Presistence.AppDb.Jobs;
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
                .WithCronSchedule(quartzJobsSchedulesOptions.AuthDb.ClearUsersActionsToConfirm)
            );

            var clearRefreshTokensJob = new JobKey("ClearRefreshTokensJob");
            q.AddJob<ClearRefreshTokensJob>(opts => opts.WithIdentity(clearRefreshTokensJob));
            q.AddTrigger(opts => opts
                .ForJob(clearRefreshTokensJob)
                .WithIdentity("ClearRefreshTokensJob-Trigger")
                .WithCronSchedule(quartzJobsSchedulesOptions.AuthDb.ClearRefreshTokens)
            );

            var calculateGlobalRankingJob = new JobKey("CalculateGlobalRankingJob");
            q.AddJob<CalculateGlobalRankingJob>(opts => opts.WithIdentity(calculateGlobalRankingJob));
            q.AddTrigger(opts => opts
                .ForJob(calculateGlobalRankingJob)
                .WithIdentity("CalculateGlobalRankingJob-Trigger")
                .WithCronSchedule(quartzJobsSchedulesOptions.AppDb.CalculateGlobalRanking)
            );

            var dailyRemoveLimitsJob = new JobKey("DailyRemoveLimitsJob");
            q.AddJob<DailyRemoveLimitsJob>(opts => opts.WithIdentity(dailyRemoveLimitsJob));
            q.AddTrigger(opts => opts
                .ForJob(dailyRemoveLimitsJob)
                .WithIdentity("DailyRemoveLimitsJob-Trigger")
                .WithCronSchedule(quartzJobsSchedulesOptions.AppDb.DailyRemoveLimits)
            );

            var weeklyRemoveLimitsJob = new JobKey("WeeklyRemoveLimitsJob");
            q.AddJob<WeeklyRemoveLimitsJob>(opts => opts.WithIdentity(weeklyRemoveLimitsJob));
            q.AddTrigger(opts => opts
                .ForJob(weeklyRemoveLimitsJob)
                .WithIdentity("WeeklyRemoveLimitsJob-Trigger")
                .WithCronSchedule(quartzJobsSchedulesOptions.AppDb.WeeklyRemoveLimits)
            );
        });
        services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);

        return services;
    }
}