using Newtonsoft.Json.Converters;

namespace TimeLogger.Converters
{
    public class CustomDateTimeConverter : IsoDateTimeConverter
    {
        public CustomDateTimeConverter()
        {
            DateTimeFormat = "yyyyMMdd";
        }
    }
}