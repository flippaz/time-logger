using System;
using System.Collections.Generic;
using TimeLogger.Models;
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
            _repository.InsertLogTime((DateTime)request.LogTime, "LogIn");
        }

        public void LogOutTime(LogTimeRequest request)
        {
            _repository.InsertLogTime((DateTime)request.LogTime, "LogOut");
        }

        public void UpdateTime(int id, LogTimeRequest request)
        {
            _repository.UpdateLogTime(id, (DateTime)request.LogTime, request.Comment);
        }
    }
}