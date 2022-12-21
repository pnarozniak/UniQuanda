using UniQuanda.Core.Application.Services;

namespace UniQuanda.Presentation.API.Middlewares
{
    public class LogMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILoggerService _logger;

        public LogMiddleware(RequestDelegate next, ILoggerService logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                var body = await new StreamReader(context.Request.Body).ReadToEndAsync();
                var headersAsString = context.Request.Headers.Select(x => $"{x.Key}: {x.Value}").Aggregate((x, y) => $"{x}, {y}");
                var endpoint = context.Request.Path;
                var client = context.Connection.RemoteIpAddress.ToString();
                await _logger.LogAppErrorAsync(ex, headersAsString, endpoint, body, client);
                throw ex;
            }
        }
    }
}
