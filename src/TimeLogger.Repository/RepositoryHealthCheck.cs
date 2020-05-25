using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using TimeLogger.Repository.Context;

namespace TimeLogger.Repository
{
    public class RepositoryHealthCheck : IHealthCheck
    {
        private readonly TimeLoggerContext _context;
        private readonly ILogger<RepositoryHealthCheck> _logger;

        public RepositoryHealthCheck(TimeLoggerContext context, ILogger<RepositoryHealthCheck> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            try
            {
                if (!(await _context.Database.CanConnectAsync(cancellationToken)))
                {
                    throw new Exception("Error connecting to database");
                }

                return await Task.FromResult(
                    HealthCheckResult.Healthy("A healthy result."));
            }
            catch (Exception exc)
            {
                _logger.LogError(0, exc, "Failed to communicate with database");

                return await Task.FromResult(
                    HealthCheckResult.Unhealthy("An unhealthy result."));
            }
        }
    }
}