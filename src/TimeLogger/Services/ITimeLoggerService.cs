using System;
using System.Collections.Generic;
using TimeLogger.Models;
using TimeLogger.Repository.Entities;

namespace TimeLogger.Services
{
    public interface ITimeLoggerService
    {
        void BulkLogTimes(IList<LogTimeRequest> request);

        void DeleteTime(int id);

        IEnumerable<Timesheet> GetTimesheet(DateTime startDate, DateTime endDate);

        void LogInTime(LogTimeRequest request);

        void LogOutTime(LogTimeRequest request);

        void UpdateTime(int id, LogTimeRequest request);
    }
}