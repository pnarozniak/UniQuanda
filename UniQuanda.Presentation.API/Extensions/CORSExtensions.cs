using UniQuanda.Presentation.API.Options;

namespace UniQuanda.Presentation.API.Extensions
{
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
                        builder.WithOrigins(corsOptions.Url)
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
}
