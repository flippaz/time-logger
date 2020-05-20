using Microsoft.EntityFrameworkCore;
using TimeLogger.Repository.Context;

namespace TimeLogger.Repository
{
    public delegate void MigrateDatabase();

    internal class DatabaseMigrator
    {
        private readonly TimeLoggerContext _context;

        public DatabaseMigrator(TimeLoggerContext context)
        {
            _context = context;
        }

        public void Migrate()
        {
            _context.Database.Migrate();
        }
    }
}