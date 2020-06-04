using Newtonsoft.Json;
using System;
using TimeLogger.Converters;

namespace TimeLogger.Models
{
    public class PublicHolidayRecord
    {
        [JsonConverter(typeof(CustomDateTimeConverter))]
        public DateTime Date { get; set; }

        [JsonProperty(PropertyName = "Holiday Name")]
        public string HolidayName { get; set; }
    }
}