using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using TimeLogger.Repository.Context;

namespace TimeLogger.Repository
{
    public class MigrationsDbContextFactory : IDesignTimeDbContextFactory<TimeLoggerContext>
    {
        public TimeLoggerContext CreateDbContext(string[] args)
        {
            return new TimeLoggerContext(
                new DbContextOptionsBuilder<TimeLoggerContext>()
                    .UseNpgsql("Server=localhost;Database=TimeLogger;User Id=postgres;Password=root")
                    .Options);
        }
    }
}