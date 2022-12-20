using Microsoft.EntityFrameworkCore;
using Quartz;
using UniQuanda.Core.Domain.Enums;

namespace UniQuanda.Infrastructure.Presistence.AppDb.Jobs
{
    class DailyRemoveLimitsJob : IJob
    {
        private readonly AppDbContext _context;
        public DailyRemoveLimitsJob(AppDbContext context)
        {
            _context = context;
        }
        public async Task Execute(IJobExecutionContext context)
        {
            var tran = await _context.Database.BeginTransactionAsync();
            var dailyEnum = (int)DurationEnum.OneDay;
            try { 
                await _context.Database.ExecuteSqlInterpolatedAsync($@"
                    UPDATE uniquanda.""PermissionUsageByUsers"" SET ""UsedTimes"" = 0 WHERE ""PermissionId"" IN (
	                    SELECT ""PermissionId"" FROM uniquanda.""RolePermissions"" WHERE ""LimitRefreshPeriod"" = {dailyEnum}
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