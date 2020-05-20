using System;
using TimeLogger.Repository.Context;
using TimeLogger.Repository.Entities;

namespace TimeLogger.Repository
{
    public class TimeLoggerRepository : ITimeLoggerRepository
    {
        private readonly TimeLoggerContext _context;

        public TimeLoggerRepository(TimeLoggerContext context)
        {
            _context = context;
        }

        public void InsertLogTime(DateTime logDateTime, string action)
        {
            _context.TimeSheets.Add(new Timesheet
            {
                Action = action,
                LogDateTime = logDateTime
            });

            _context.SaveChanges();
        }
    }
}