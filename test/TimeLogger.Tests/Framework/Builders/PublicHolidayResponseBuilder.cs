using TimeLogger.Models;

namespace TimeLogger.Tests.Framework.Builders
{
    public class PublicHolidayResponseBuilder : PublicHolidayResponse
    {
        public PublicHolidayResponseBuilder()
        {
            WithResult(new PublicHolidayResultBuilder());
            WithSuccess(RandomBuilder.NextBool());
        }

        public PublicHolidayResponseBuilder WithResult(PublicHolidayResult result)
        {
            Result = result;

            return this;
        }

        public PublicHolidayResponseBuilder WithSuccess(bool success)
        {
            Success = success;

            return this;
        }
    }
}