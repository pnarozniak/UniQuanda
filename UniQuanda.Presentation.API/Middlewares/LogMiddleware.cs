using UniQuanda.Core.Application.Services;

namespace UniQuanda.Presentation.API.Middlewares
{
    public class LogMiddleware
    {
        private readonly RequestDelegate _next;
        public LogMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, ILoggerService logger)
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
                var queryParams = context.Request.QueryString.Value;
                await logger.LogAppErrorAsync(ex, headersAsString, endpoint, body, client, queryParams);
                throw ex;
            }
        }
    }
}
