using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Npgsql;
using System;
using TimeLogger.Repository.Context;

namespace TimeLogger.Repository
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddRepository(this IServiceCollection services)
        {
            return services
                .AddRepository(
                    (provider, builder) =>
                        builder.UseNpgsql(ConstructConnectionString(provider)));
        }

        private static string ConstructConnectionString(IServiceProvider provider)
        {
            return new NpgsqlConnectionStringBuilder()
            {
                Host = provider.GetRequiredService<IOptions<RepositorySettings>>().Value.ConnectionString.PostgresServer,
                Database = "TimeLogger",
                Username = provider.GetRequiredService<IOptions<RepositorySettings>>().Value.ConnectionString.PostgresUser,
                Password = provider.GetRequiredService<IOptions<RepositorySettings>>().Value.ConnectionString.PostgresPassword
            }.ToString();
        }

        public static IServiceCollection AddRepository(this IServiceCollection services, Action<IServiceProvider, DbContextOptionsBuilder> optionsAction)
        {
            return services
                .AddDbContextPool<TimeLoggerContext>(optionsAction)
                .AddScoped<DatabaseMigrator>()
                .AddSingleton<MigrateDatabase>(provider => () =>
                {
                    using (IServiceScope scope = provider.CreateScope())
                    {
                        scope.ServiceProvider.GetRequiredService<DatabaseMigrator>().Migrate();
                    }
                })
                .AddScoped<ITimeLoggerRepository, TimeLoggerRepository>();
        }
    }
}