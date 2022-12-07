using Microsoft.EntityFrameworkCore;
using Quartz;

namespace UniQuanda.Infrastructure.Presistence.AuthDb.Jobs;

public class ClearUsersActionsToConfirmJob : IJob
{
    private AuthDbContext _authDbContext;
    public ClearUsersActionsToConfirmJob(AuthDbContext authDbContext)
    {
        _authDbContext = authDbContext;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        var now = DateTime.UtcNow;
        await _authDbContext.Database.ExecuteSqlRawAsync(@$"
					DELETE FROM {"\"UsersActionsToConfirm\""}
					WHERE {"\"ExistsUntil\""} >= '{now}'"
        );
        await _authDbContext.SaveChangesAsync();
    }
}