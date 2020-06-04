using System;
using TimeLogger.Models;

namespace TimeLogger.Tests.Framework.Builders
{
    public class LogTimeRequestBuilder : LogTimeRequest
    {
        public LogTimeRequestBuilder()
        {
            WithComment(RandomBuilder.NextString());
            WithLogAction(RandomBuilder.NextString());
            WithLogTime(RandomBuilder.NextDateTime());
            WithOverrideHolidays(RandomBuilder.NextBool());
        }

        public LogTimeRequestBuilder WithComment(string comment)
        {
            Comment = comment;

            return this;
        }

        public LogTimeRequestBuilder WithLogAction(string logAction)
        {
            LogAction = logAction;

            return this;
        }

        public LogTimeRequestBuilder WithLogTime(DateTime? logTime)
        {
            LogTime = logTime;

            return this;
        }

        public LogTimeRequestBuilder WithOverrideHolidays(bool? overrideHolidays)
        {
            OverrideHolidays = overrideHolidays;

            return this;
        }
    }
}