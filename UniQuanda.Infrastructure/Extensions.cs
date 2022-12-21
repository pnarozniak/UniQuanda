using Amazon.Extensions.NETCore.Setup;
using Amazon.Runtime;
using Amazon.S3;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UniQuanda.Core.Application.Repositories;
using UniQuanda.Core.Application.Services;
using UniQuanda.Core.Application.Services.Auth;
using UniQuanda.Infrastructure.HostedServices;
using UniQuanda.Infrastructure.Options;
using UniQuanda.Infrastructure.Repositories;
using UniQuanda.Infrastructure.Services;
using UniQuanda.Infrastructure.Services.Auth;

namespace UniQuanda.Infrastructure;

public static class Extensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        // Options
        services.AddSingleton(new SendGridOptions(configuration));
        services.AddSingleton(new DataExpirationOptions(configuration));
        services.AddSingleton(new TokensOptions(configuration));
        services.AddSingleton(new UniQuandaClientOptions(configuration));
        services.AddSingleton(new OAuthOptions(configuration));
        services.AddSingleton(new PayUOptions(configuration));

        // Repositories
        services.AddScoped<IAuthRepository, AuthRepository>();
        services.AddScoped<IAppUserRepository, AppUserRepository>();
        services.AddScoped<ITagRepository, TagRepository>();
        services.AddScoped<IReportRepository, ReportRepository>();
        services.AddScoped<IQuestionRepository, QuestionRepository>();
        services.AddScoped<IContentRepository, ContentRepository>();
        services.AddScoped<IRankingRepository, RankingRepository>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IPremiumPaymentRepository, PremiumPaymentRepository>();
        services.AddScoped<IAnswerRepository, AnswerRepository>();
        services.AddScoped<ISearchRepository, SearchRepository>();
        services.AddScoped<ITestRepository, TestRepository>();
        services.AddScoped<IUniversityRepository, UniversityRepository>();
        services.AddScoped<IAcademicTitleRepository, AcademicTitleRepository>();
        services.AddScoped<IRoleRepository, RoleRepository>();
        services.AddScoped<IAnswerRepository, AnswerRepository>();

        // Services
        services.AddScoped<IPasswordsService, PasswordsService>();
        services.AddScoped<ITokensService, TokensService>();
        services.AddScoped<IEmailService, SendGridService>();
        services.AddScoped<IExpirationService, ExpirationService>();
        services.AddScoped<ICacheService, CacheService>();
        services.AddScoped<IImageService, ImageService>();
        services.AddScoped<IOAuthService, OAuthService>();
        services.AddScoped<IHtmlService, HtmlService>();
        services.AddScoped<IPaymentService, PayUService>();
        services.AddScoped<ILoggerService, LoggerService>();

        // Cache
        services.AddStackExchangeRedisCache(options =>
        {
            var cacheOptions = new CacheOptions(configuration);
            options.Configuration = cacheOptions.ConnectionString;
            options.InstanceName = "UniQuanda-";
        });

        //AWS S3
        services.AddDefaultAWSOptions(new AWSOptions
        {
            Credentials = new BasicAWSCredentials(
                configuration.GetSection("AWS")["AccessKeyId"],
                configuration.GetSection("AWS")["SecretAccessKey"]),
            Region = Amazon.RegionEndpoint.GetBySystemName(configuration.GetSection("AWS")["Region"])
        });
        services.AddAWSService<IAmazonS3>();

        //Hosted Services
        services.AddHostedService<CalculateRankingOnStartup>();

        return services;
    }
}