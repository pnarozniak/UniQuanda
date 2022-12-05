using Microsoft.EntityFrameworkCore;
using Quartz;

namespace UniQuanda.Infrastructure.Presistence.AuthDb.Jobs;

public class ClearTempUsersJob : IJob
{
		private AuthDbContext _authDbContext;
		public ClearTempUsersJob(AuthDbContext authDbContext) 
		{
				_authDbContext = authDbContext;
		}

		public async Task Execute(IJobExecutionContext context)
		{
				var now = DateTime.UtcNow;
				await _authDbContext.Database.ExecuteSqlRawAsync(@$"
					DELETE FROM {"\"Users\""}
					USING {"\"TempUsers\""}
					WHERE {"\"Users\""}.{"\"Id\""} = {"\"TempUsers\""}.{"\"IdUser\""}
					AND {"\"TempUsers\""}.{"\"ExistsUntil\""} >= '{now}'"
				);

				await _authDbContext.SaveChangesAsync();
		}
}