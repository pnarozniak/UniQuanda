using Microsoft.EntityFrameworkCore;
using Quartz;

namespace UniQuanda.Infrastructure.Presistence.AuthDb.Jobs;

public class ClearRefreshTokensJob : IJob
{
    private readonly AuthDbContext _authDbContext;
    public ClearRefreshTokensJob(AuthDbContext authDbContext)
    {
        _authDbContext = authDbContext;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        var now = DateTime.UtcNow;

        await _authDbContext.Database.ExecuteSqlRawAsync(@$"
					UPDATE {"\"Users\""} SET
					{"\"RefreshToken\""} = NULL, {"\"RefreshTokenExp\""} = NULL
					WHERE {"\"RefreshTokenExp\""} >= '{now}'"
        );

        await _authDbContext.SaveChangesAsync();
    }
}