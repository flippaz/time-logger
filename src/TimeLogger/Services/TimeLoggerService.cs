using System;
using System.Collections.Generic;
using System.Linq;
using TimeLogger.Models;
using TimeLogger.Reference;
using TimeLogger.Repository;
using TimeLogger.Repository.Entities;

namespace TimeLogger.Services
{
    public class TimeLoggerService : ITimeLoggerService
    {
        private readonly ITimeLoggerRepository _repository;

        public TimeLoggerService(ITimeLoggerRepository repository)
        {
            _repository = repository;
        }

        public void BulkLogTimes(IList<LogTimeRequest> request)
        {
            var timeSheet = request
                .Select(r =>
                    new Timesheet
                    {
                        Action = Enum.TryParse(r.LogAction, true, out LogAction action) ? action.ToString() : "Unknown",
                        LogDateTime = (DateTime)r.LogTime,
                        Comments = r.Comment
                    })
                .ToList();

            _repository.InsertLogTimes(timeSheet);
        }

        public void DeleteTime(int id)
        {
            _repository.DeleteTime(id);
        }

        public IEnumerable<Timesheet> GetTimesheet(DateTime startDate, DateTime endDate)
        {
            return _repository.GetTimesheet(startDate, endDate);
        }

        public void LogInTime(LogTimeRequest request)
        {
            _repository.InsertLogTime((DateTime)request.LogTime, LogAction.LogIn.ToString());
        }

        public void LogOutTime(LogTimeRequest request)
        {
            _repository.InsertLogTime((DateTime)request.LogTime, LogAction.LogOut.ToString());
        }

        public void UpdateTime(int id, LogTimeRequest request)
        {
            _repository.UpdateLogTime(id, (DateTime)request.LogTime, request.Comment);
        }
    }
}