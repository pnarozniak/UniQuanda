using Microsoft.Extensions.DependencyInjection;
using UniQuanda.Core.Application.Repositories;
using UniQuanda.Infrastructure.Repositories;

namespace UniQuanda.Infrastructure
{
    public static class Extensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
            => services.AddScoped<IQuestionRepository, QuestionRepository>();
    }
}
