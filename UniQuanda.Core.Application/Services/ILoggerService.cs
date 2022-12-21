namespace UniQuanda.Core.Application.Services
{
    public interface ILoggerService
    {
        public Task LogAppErrorAsync(Exception ex, string headers, string endpoint, string? body, string? client, string? queryParams);
    }
}
