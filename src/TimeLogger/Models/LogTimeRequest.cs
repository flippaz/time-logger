using System;

namespace TimeLogger.Models
{
    public class LogTimeRequest
    {
        public string Comment { get; set; }

        public DateTime? LogTime { get; set; }
    }
}