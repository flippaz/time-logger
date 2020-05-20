using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TimeLogger.Repository.Entities;

namespace TimeLogger.Repository.Context
{
    public class TimeLoggerContext : DbContext
    {
        public TimeLoggerContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Timesheet> TimeSheets { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            EntityTypeBuilder<Timesheet> betEntity = builder.Entity<Timesheet>();

            betEntity
                .Property(e => e.LogDateTime)
                .HasDefaultValueSql("current_timestamp");

            betEntity.HasIndex(e => e.Id).IsUnique();

            base.OnModelCreating(builder);
        }
    }
}