using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TimeLogger.Repository;
using TimeLogger.Services;

namespace TimeLogger.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddServiceOptions(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddOptions()
                    .Configure<RepositorySettings>(configuration.GetSection("RepositorySettings"));

            return services;
        }

        public static IServiceCollection AddTimeLoggerService(this IServiceCollection services)
        {
            return services.AddSingleton<ITimeLoggerService, TimeLoggerService>();
        }
    }
}