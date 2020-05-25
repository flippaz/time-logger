using System;
using System.Collections.Generic;
using TimeLogger.Repository.Entities;

namespace TimeLogger.Repository
{
    public interface ITimeLoggerRepository
    {
        void InsertLogTimes(List<Timesheet> request);

        void DeleteTime(int id);

        IEnumerable<Timesheet> GetTimesheet(DateTime startDate, DateTime endDate);

        void InsertLogTime(DateTime logDateTime, string action);

        void UpdateLogTime(int id, DateTime logDateTime, string comment);
    }
}