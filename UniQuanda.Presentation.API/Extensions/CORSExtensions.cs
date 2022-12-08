using UniQuanda.Presentation.API.Options;

namespace UniQuanda.Presentation.API.Extensions;

public static class CORSExtensions
{
    private const string PolicyName = "AllowOrigins";

    public static IServiceCollection AddCORS(this IServiceCollection services, IConfiguration configuration)
    {
        var corsOptions = new CORSOptions(configuration);
        services.AddSingleton(corsOptions);

        services.AddCors(opt =>
        {
            opt.AddPolicy(PolicyName,
                builder =>
                {
                    builder.WithOrigins(corsOptions.Url,
                        "185.68.14.10", "185.68.14.11", "185.68.14.12", "185.68.14.26", "185.68.14.27", "185.68.14.28")
                        .AllowCredentials()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                });
        });

        return services;
    }

    public static IApplicationBuilder UseCORS(this IApplicationBuilder app)
    {
        app.UseCors(PolicyName);

        return app;
    }
}