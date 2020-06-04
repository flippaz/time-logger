using System;

namespace TimeLogger.Models
{
    public class LogTimeRequest
    {
        public string Comment { get; set; }

        public string LogAction { get; set; }

        public DateTime? LogTime { get; set; }

        public bool? OverrideHolidays { get; set; }
    }
}