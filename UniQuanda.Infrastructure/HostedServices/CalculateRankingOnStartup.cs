using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using UniQuanda.Infrastructure.Presistence.AppDb;

namespace UniQuanda.Infrastructure.HostedServices
{
    public class CalculateRankingOnStartup : IHostedService
    {
        private readonly AppDbContext _context;
        public CalculateRankingOnStartup(IServiceProvider serviceProvider)
        {
            _context = serviceProvider.CreateScope().ServiceProvider.GetRequiredService<AppDbContext>();
        }
        public async Task StartAsync(CancellationToken ct)
        {
            var tran = await _context.Database.BeginTransactionAsync(ct);
            await _context.Database.ExecuteSqlRawAsync("TRUNCATE uniquanda.\"GlobalRankings\"");
            await _context.Database.ExecuteSqlRawAsync(@"
                INSERT INTO uniquanda.""GlobalRankings"" (""Place"", ""AppUserId"", ""Points"")
                    SELECT ROW_NUMBER() OVER (ORDER BY SUM(""Points"") DESC) AS Place, ""AppUserId"", SUM(""Points"") AS Points
                    FROM uniquanda.""UsersPointsInTags""
                    GROUP BY ""AppUserId""
                ORDER BY SUM(""Points"") DESC
            ", ct);
            await tran.CommitAsync(ct);
        }

        public Task StopAsync(CancellationToken ct)
        {
            return Task.CompletedTask;
        }
    }
}
