using System;
using TimeLogger.Models;
using TimeLogger.Repository;

namespace TimeLogger.Services
{
    public class TimeLoggerService : ITimeLoggerService
    {
        private readonly ITimeLoggerRepository _repository;

        public TimeLoggerService(ITimeLoggerRepository repository)
        {
            _repository = repository;
        }

        public void LogInTime(LogTimeRequest request)
        {
            _repository.InsertLogTime((DateTime)request.LogTime, "LogIn");
        }

        public void LogOutTime(LogTimeRequest request)
        {
            _repository.InsertLogTime((DateTime)request.LogTime, "LogOut");
        }
    }
}