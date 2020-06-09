using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeLogger.Clients;
using TimeLogger.Models;
using TimeLogger.Reference;
using TimeLogger.Repository;
using TimeLogger.Repository.Entities;

namespace TimeLogger.Services
{
    public class TimeLoggerService : ITimeLoggerService
    {
        private readonly ILogger<TimeLoggerService> _logger;
        private readonly IPublicHolidayClient _publicHolidayClient;
        private readonly ITimeLoggerRepository _repository;

        public TimeLoggerService(ITimeLoggerRepository repository, IPublicHolidayClient publicHolidayClient, ILogger<TimeLoggerService> logger)
        {
            _repository = repository;
            _publicHolidayClient = publicHolidayClient;
            _logger = logger;
        }

        public async Task BulkLogTimes(BulkLogTimesRequest request)
        {
            foreach (DateTime day in EachDay(request.FromDateTime, request.ToDateTime))
            {
                await LogInTime(new LogTimeRequest
                {
                    LogTime = new DateTime(day.Year, day.Month, day.Day, 9, 0, 0),
                    OverrideHolidays = request.OverrideHolidays
                });

                await LogOutTime(new LogTimeRequest
                {
                    LogTime = new DateTime(day.Year, day.Month, day.Day, 17, 0, 0),
                    OverrideHolidays = request.OverrideHolidays
                });
            }
        }

        public void DeleteTime(int id)
        {
            _repository.DeleteTime(id);
        }

        public IEnumerable<Timesheet> GetTimesheet(DateTime startDate, DateTime endDate)
        {
            return _repository.GetTimesheet(startDate, endDate);
        }

        public async Task LogInTime(LogTimeRequest request)
        {
            if (!await CheckIsPublicHolidayOrWeekend((DateTime)request.LogTime, request.OverrideHolidays))
            {
                _repository.InsertLogTime((DateTime)request.LogTime, LogAction.LogIn.ToString());
            }
        }

        public async Task LogMultipleTimes(IList<LogTimeRequest> requests)
        {
            var bulkTimeSheets = new List<Timesheet>();

            foreach (LogTimeRequest request in requests)
            {
                if (Enum.TryParse(request.LogAction, true, out LogAction action)
                    && !await CheckIsPublicHolidayOrWeekend((DateTime)request.LogTime, request.OverrideHolidays))
                {
                    bulkTimeSheets.Add
                    (
                        new Timesheet
                        {
                            Action = action.ToString(),
                            LogDateTime = (DateTime)request.LogTime,
                            Comments = request.Comment
                        }
                    );
                }
            }

            _repository.InsertLogTimes(bulkTimeSheets);
        }

        public async Task LogOutTime(LogTimeRequest request)
        {
            if (!await CheckIsPublicHolidayOrWeekend((DateTime)request.LogTime, request.OverrideHolidays))
            {
                _repository.InsertLogTime((DateTime)request.LogTime, LogAction.LogOut.ToString());
            }
        }

        public void UpdateTime(int id, LogTimeRequest request)
        {
            _repository.UpdateLogTime(id, (DateTime)request.LogTime, request.Comment);
        }

        private async Task<bool> CheckIsPublicHolidayOrWeekend(DateTime dateTime, bool? overrideHolidaysWeekends)
        {
            if (overrideHolidaysWeekends ?? false)
            {
                return false;
            }

            if ((dateTime.DayOfWeek == DayOfWeek.Saturday) || (dateTime.DayOfWeek == DayOfWeek.Sunday))
            {
                return true;
            }

            var holidays = await _publicHolidayClient.GetHolidays();

            if (holidays.Success)
            {
                _logger.LogInformation("Success!");
                return holidays.Result.Records.Any(h => h.Date.Date == dateTime.Date);
            }

            throw new Exception("Error occurred");
        }

        private IEnumerable<DateTime> EachDay(DateTime from, DateTime through)
        {
            for (var day = from.Date; day.Date <= through.Date; day = day.AddDays(1))
                yield return day;
        }
    }
}