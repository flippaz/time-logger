using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using TimeLogger.Reference;

namespace TimeLogger.Models
{
    public class LogTimeRequest
    {
        public string Comment { get; set; }

        public string LogAction { get; set; }

        public DateTime? LogTime { get; set; }
    }
}