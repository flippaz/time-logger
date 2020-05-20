using TimeLogger.Models;

namespace TimeLogger.Services
{
    public interface ITimeLoggerService
    {
        public void LogInTime(LogTimeRequest request);

        public void LogOutTime(LogTimeRequest request);
    }
}