using Microsoft.Extensions.Configuration;

namespace UniQuanda.Infrastructure.Presistence.Options;

public class QuartzJobsSchedulesOptions
{
    public QuartzJobsSchedulesOptions(IConfiguration configuration)
    {
        AuthDb = new AuthDbQuartzJobsSchedulesOptions(configuration.GetSection("QuartzJobsSchedules:AuthDb"));
    }

    public AuthDbQuartzJobsSchedulesOptions AuthDb { get; set; }
}

public class AuthDbQuartzJobsSchedulesOptions
{
    public AuthDbQuartzJobsSchedulesOptions(IConfigurationSection section)
    {
        ClearRefreshTokens = section["ClearRefreshTokens"];
        ClearTempUsers = section["ClearTempUsers"];
        ClearUserActionsToConfirm = section["ClearUserActionsToConfirm"];
    }

    public string ClearRefreshTokens { get; set; }
    public string ClearTempUsers { get; set; }
    public string ClearUserActionsToConfirm { get; set; }
}
