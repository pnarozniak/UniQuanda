using UniQuanda.Core.Application.Services;
using UniQuanda.Presentation.API.Middlewares;

namespace UniQuanda.Presentation.API.Extensions
{
    public static class LoggerExtensions
    {
        public static IApplicationBuilder UseLogger(this IApplicationBuilder app)
        {
            app.UseMiddleware<LogMiddleware>();
            return app;
        }
    }
}
