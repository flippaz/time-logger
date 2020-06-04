using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Reflection;
using TimeLogger.Clients;
using TimeLogger.Repository;
using TimeLogger.Services;
using TimeLogger.Settings;

namespace TimeLogger.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddPublicHolidayClient(this IServiceCollection services)
        {
            return services
                .AddHttpClient()
                .AddSingleton<IPublicHolidayClient, PublicHolidayClient>();
        }

        public static IServiceCollection AddServiceOptions(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddOptions()
                .Configure<RepositorySettings>(configuration)
                .Configure<PublicHolidaySettings>(configuration.GetSection("PublicHolidaySettings"));

            return services;
        }

        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(
                    "v1",
                    new OpenApiInfo
                    {
                        Title = "Time Logger",
                        Version = "v1",
                        Contact = new OpenApiContact
                        {
                            Name = "Fred Rizardo"
                        }
                    }
                );

                string xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                string xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

                if (File.Exists(xmlPath))
                {
                    c.IncludeXmlComments(xmlPath);
                }
            });

            return services;
        }

        public static IServiceCollection AddTimeLoggerService(this IServiceCollection services)
        {
            return services.AddScoped<ITimeLoggerService, TimeLoggerService>();
        }
    }
}