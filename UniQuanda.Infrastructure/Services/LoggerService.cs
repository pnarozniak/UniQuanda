using UniQuanda.Core.Application.Services;
using UniQuanda.Infrastructure.Presistence.AppDb;
using UniQuanda.Infrastructure.Presistence.AppDb.Models;

namespace UniQuanda.Infrastructure.Services
{
    public class LoggerService : ILoggerService
    {
        private readonly AppDbContext _context;

        public LoggerService(AppDbContext context)
        {
            _context = context;
        }

        public async Task LogAppErrorAsync(Exception ex, string headers, string endpoint, string? body, string? client, string? queryParams)
        {
            var log = new Log
            {
                Exception = ex.Message,
                StackTrace = ex.StackTrace ?? "",
                Endpoint = endpoint,
                Body = body,
                Headers = headers,
                Client = client,
                QueryParams = queryParams,
                CreatedAt = DateTime.UtcNow
            };

            await _context.Logs.AddAsync(log);
            await _context.SaveChangesAsync();
        }
    }
}
