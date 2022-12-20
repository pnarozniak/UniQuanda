using Microsoft.EntityFrameworkCore;
using Quartz;
using UniQuanda.Core.Domain.Enums;

namespace UniQuanda.Infrastructure.Presistence.AppDb.Jobs
{
    class WeeklyRemoveLimitsJob : IJob
    {
        private readonly AppDbContext _context;
        public WeeklyRemoveLimitsJob(AppDbContext context)
        {
            _context = context;
        }
        public async Task Execute(IJobExecutionContext context)
        {
            var tran = await _context.Database.BeginTransactionAsync();
            var weeklyEnum = (int)DurationEnum.OneWeek;
            try { 
                await _context.Database.ExecuteSqlInterpolatedAsync($@"
                    UPDATE uniquanda.""PermissionUsageByUsers"" SET ""UsedTimes"" = 0 WHERE ""PermissionId"" IN (
	                    SELECT ""PermissionId"" FROM uniquanda.""RolePermissions"" WHERE ""LimitRefreshPeriod"" = {weeklyEnum}
                    );
                ");
                await tran.CommitAsync();
            } catch
            {
                await tran.RollbackAsync();
            }
        }
    }
}