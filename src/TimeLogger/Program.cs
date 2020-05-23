using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;
using Serilog.Extensions.Logging;
using Serilog.Formatting.Json;
using Serilog.Sinks.RollingFileAlternate;
using System.IO;
using System.Net;

namespace TimeLogger
{
    public class Program
    {
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder
                        .UseContentRoot(Directory.GetCurrentDirectory())
                        .UseUrls("http://*:5000")
                        .UseKestrel()
                        .UseStartup<Startup>()
                        .UseDefaultServiceProvider(options => options.ValidateScopes = true)
                        .ConfigureAppConfiguration(
                            (hostingContext, config) => config
                                .AddEnvironmentVariables("TimeLogger_")
                                .AddJsonFile($"appsettings.{hostingContext.HostingEnvironment.EnvironmentName}.json", true)
                                .AddCommandLine(args))
                        .ConfigureLogging((hostingContext, logging) =>
                            logging.AddProvider(CreateLoggerProvider(hostingContext.Configuration)));
                });

        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        private static SerilogLoggerProvider CreateLoggerProvider(IConfiguration configuration)
        {
            LoggerConfiguration logConfig = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .MinimumLevel.Warning()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                .MinimumLevel.Override("System", LogEventLevel.Warning)
                .WriteTo.RollingFileAlternate(new JsonFormatter(), "./logs", fileSizeLimitBytes: 100000000, retainedFileCountLimit: 30)
                .WriteTo.Console(new JsonFormatter())
                .ReadFrom.Configuration(configuration)
                .Enrich.WithProperty("Hostname", Dns.GetHostName());

            return new SerilogLoggerProvider(logConfig.CreateLogger());
        }
    }
}