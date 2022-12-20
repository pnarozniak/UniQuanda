using Microsoft.Extensions.Configuration;

namespace UniQuanda.Infrastructure.Presistence.Options;

public class QuartzJobsSchedulesOptions
{
    public QuartzJobsSchedulesOptions(IConfiguration configuration)
    {
        AuthDb = new AuthDbQuartzJobsSchedulesOptions(configuration.GetSection("QuartzJobsSchedules:AuthDb"));
        AppDb = new AppDbQuartzJobsSchedulesOptions(configuration.GetSection("QuartzJobsSchedules:AppDb"));
    }

    public AuthDbQuartzJobsSchedulesOptions AuthDb { get; set; }
    public AppDbQuartzJobsSchedulesOptions AppDb { get; set; }
}

public class AuthDbQuartzJobsSchedulesOptions
{
    public AuthDbQuartzJobsSchedulesOptions(IConfigurationSection section)
    {
        ClearRefreshTokens = section["ClearRefreshTokens"];
        ClearTempUsers = section["ClearTempUsers"];
        ClearUsersActionsToConfirm = section["ClearUsersActionsToConfirm"];
    }

    public string ClearRefreshTokens { get; set; }
    public string ClearTempUsers { get; set; }
    public string ClearUsersActionsToConfirm { get; set; }
}

public class AppDbQuartzJobsSchedulesOptions
{
    public AppDbQuartzJobsSchedulesOptions(IConfigurationSection section)
    {
        CalculateGlobalRanking = section["CalculateGlobalRanking"];
        DailyRemoveLimits = section["DailyRemoveLimits"];
        WeeklyRemoveLimits = section["WeeklyRemoveLimits"];
    }

    public string CalculateGlobalRanking { get; set; }
    public string DailyRemoveLimits { get; set; }
    public string WeeklyRemoveLimits { get; set; }
}