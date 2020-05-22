using System;
using System.Collections.Generic;
using System.Linq;
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

        public void DeleteTime(int id)
        {
            Timesheet timeSheet = _context.TimeSheets.SingleOrDefault(t => t.Id == id);

            if (timeSheet != null)
            {
                _context.Remove(timeSheet);
                _context.SaveChanges();
            }
        }

        public IEnumerable<Timesheet> GetTimesheet(DateTime startDate, DateTime endDate)
        {
            return _context.TimeSheets
                .Where(t =>
                    t.LogDateTime > startDate.Date
                    && t.LogDateTime < endDate.Date);
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

        public void UpdateLogTime(int id, DateTime logDateTime, string comment)
        {
            var timeSheet = _context.TimeSheets.SingleOrDefault(t => t.Id == id);

            if (timeSheet == null)
            {
                return;
            }

            timeSheet.LogDateTime = logDateTime;
            timeSheet.Comments = comment;
            timeSheet.ModifiedDateTime = DateTime.Now.ToLocalTime();

            _context.SaveChanges();
        }
    }
}