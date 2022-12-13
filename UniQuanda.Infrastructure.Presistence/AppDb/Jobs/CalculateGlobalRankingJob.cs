using Microsoft.EntityFrameworkCore;
using Quartz;

namespace UniQuanda.Infrastructure.Presistence.AppDb.Jobs
{
    class CalculateGlobalRankingJob : IJob
    {
        private readonly AppDbContext _context;
        public CalculateGlobalRankingJob(AppDbContext context)
        {
            _context = context;
        }
        public async Task Execute(IJobExecutionContext context)
        {
            var tran = await _context.Database.BeginTransactionAsync();
            try { 
                await _context.Database.ExecuteSqlRawAsync("TRUNCATE uniquanda.\"GlobalRankings\"");
                await _context.Database.ExecuteSqlRawAsync(@"
                    INSERT INTO uniquanda.""GlobalRankings"" (""Place"", ""AppUserId"", ""Points"")
                        SELECT ROW_NUMBER() OVER (ORDER BY SUM(""Points"") DESC) AS Place, ""AppUserId"", SUM(""Points"") AS Points
                        FROM uniquanda.""UsersPointsInTags""
                        GROUP BY ""AppUserId""
                     ORDER BY SUM(""Points"") DESC
                ");
                await tran.CommitAsync();
            } catch
            {
                await tran.RollbackAsync();
            }
        }
    }
}