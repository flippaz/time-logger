using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using TimeLogger.Extensions;
using TimeLogger.Repository;

namespace TimeLogger
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, MigrateDatabase migrateDatabase, ILogger<Startup> logger)
        {
            try
            {
                migrateDatabase();
            }
            catch (Exception exc)
            {
                logger.LogError(0, exc, "Error during database migration");

                throw;
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Time Logger");
            });

            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHealthChecks("/health");
            });
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddHealthChecks()
                .AddCheck<RepositoryHealthCheck>("Repository Connectivity");

            services
                .AddServiceOptions(Configuration)
                .AddTimeLoggerService()
                .AddRepository()
                .AddSwagger();

            services.ConfigureSwaggerGen(options =>
                options.CustomSchemaIds(x => x.FullName));
        }
    }
}