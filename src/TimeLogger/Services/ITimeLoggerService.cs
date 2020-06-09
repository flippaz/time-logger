using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TimeLogger.Models;
using TimeLogger.Repository.Entities;

namespace TimeLogger.Services
{
    public interface ITimeLoggerService
    {
        Task BulkLogTimes(BulkLogTimesRequest request);

        void DeleteTime(int id);

        IEnumerable<Timesheet> GetTimesheet(DateTime startDate, DateTime endDate);

        Task LogInTime(LogTimeRequest request);

        Task LogMultipleTimes(IList<LogTimeRequest> request);

        Task LogOutTime(LogTimeRequest request);

        void UpdateTime(int id, LogTimeRequest request);
    }
}