using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using System;
using TimeLogger.Controllers;
using TimeLogger.Extensions;
using TimeLogger.Repository;
using TimeLogger.Settings;
using TimeLogger.Tests.Framework.Builders;

namespace TimeLogger.Tests.Framework
{
    public class TestServiceProvider
    {
        public static IServiceProvider CreateProvider(Action<IServiceCollection> overrides = null)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder().Build();

            var eventApiHttpSettings = new RepositorySettings
            {
                ConnectionString = new ConnectionString
                {
                    PostgresUser = RandomBuilder.NextString(),
                    PostgresPassword = RandomBuilder.NextString(),
                    PostgresServer = RandomBuilder.NextString()
                }
            };

            var priceLadderSettings = new PublicHolidaySettings
            {
                ApiUrl = RandomBuilder.NextString(),
                ResourceId = RandomBuilder.NextString(),
                State = RandomBuilder.NextString()
            };

            IServiceCollection services = new ServiceCollection()
                .AddServiceOptions(configuration)
                .AddTimeLoggerService()
                .AddRepository((_, builder) => builder.UseInMemoryDatabase(Guid.NewGuid().ToString()))
                .AddPublicHolidayClient()
                .Replace(ServiceDescriptor.Singleton<IOptions<RepositorySettings>>(new OptionsWrapper<RepositorySettings>(eventApiHttpSettings)))
                .Replace(ServiceDescriptor.Singleton<IOptions<PublicHolidaySettings>>(new OptionsWrapper<PublicHolidaySettings>(priceLadderSettings)))
                .AddTransient<TimesheetController>();

            overrides?.Invoke(services);

            return services.BuildServiceProvider(true);
        }
    }
}