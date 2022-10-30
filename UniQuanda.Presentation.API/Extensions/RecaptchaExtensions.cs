using UniQuanda.Presentation.API.Middlewares;
using UniQuanda.Presentation.API.Options;

namespace UniQuanda.Presentation.API.Extensions;

public static class RecaptchaExtensions
{
    public static IServiceCollection AddRecaptcha(this IServiceCollection services, IConfiguration configuration)
    {
        var recaptchaOptions = new RecaptchaOptions(configuration);
        services.AddSingleton(recaptchaOptions);

        return services;
    }

    public static IApplicationBuilder UseRecaptcha(this IApplicationBuilder app)
    {
        app.UseMiddleware<RecaptchaMidldeware>();
        return app;
    }

    public static string? GetRecaptcha(this IHeaderDictionary header)
    {
        return header["recaptcha"];
    }
}