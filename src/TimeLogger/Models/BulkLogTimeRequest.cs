using System;

namespace TimeLogger.Models
{
    public class BulkLogTimesRequest
    {
        public DateTime FromDateTime { get; set; }

        public bool? OverrideHolidays { get; set; }

        public DateTime ToDateTime { get; set; }
    }
}