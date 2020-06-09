using System;
using TimeLogger.Models;

namespace TimeLogger.Tests.Framework.Builders
{
    public class BulkLogTimesRequestBuilder : BulkLogTimesRequest
    {
        public BulkLogTimesRequestBuilder()
        {
            WithFromDateTime(RandomBuilder.NextDateTime());
            WithOverrideHolidays(RandomBuilder.NextBool());
            WithToDateTime(RandomBuilder.NextDateTime());
        }

        public BulkLogTimesRequestBuilder WithFromDateTime(DateTime fromDateTime)
        {
            FromDateTime = fromDateTime;

            return this;
        }

        public BulkLogTimesRequestBuilder WithOverrideHolidays(bool? overrideHolidays)
        {
            OverrideHolidays = overrideHolidays;

            return this;
        }

        public BulkLogTimesRequestBuilder WithToDateTime(DateTime toDateTime)
        {
            ToDateTime = toDateTime;

            return this;
        }
    }
}