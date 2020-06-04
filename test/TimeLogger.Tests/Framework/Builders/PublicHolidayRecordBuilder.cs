using System;
using TimeLogger.Models;

namespace TimeLogger.Tests.Framework.Builders
{
    public class PublicHolidayRecordBuilder : PublicHolidayRecord
    {
        public PublicHolidayRecordBuilder()
        {
            WithDate(RandomBuilder.NextDateTime());
            WithHolidayName(RandomBuilder.NextString());
        }

        public PublicHolidayRecordBuilder WithDate(DateTime date)
        {
            Date = date;

            return this;
        }

        public PublicHolidayRecordBuilder WithHolidayName(string holidayName)
        {
            HolidayName = holidayName;

            return this;
        }
    }
}