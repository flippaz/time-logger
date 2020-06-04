using System.Linq;
using TimeLogger.Models;

namespace TimeLogger.Tests.Framework.Builders
{
    public class PublicHolidayResultBuilder : PublicHolidayResult
    {
        public PublicHolidayResultBuilder()
        {
            WithRecords(new PublicHolidayRecordBuilder());
        }

        public PublicHolidayResultBuilder WithRecords(params PublicHolidayRecord[] records)
        {
            Records = records.ToList();

            return this;
        }
    }
}