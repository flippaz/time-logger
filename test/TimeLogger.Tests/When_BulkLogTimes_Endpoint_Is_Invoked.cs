using System;
using System.Linq;
using System.Threading.Tasks;
using TimeLogger.Tests.Doubles;
using TimeLogger.Tests.Fixtures;
using TimeLogger.Tests.Framework.Builders;
using Xunit;

namespace TimeLogger.Tests
{
    public class When_BulkLogTimes_Endpoint_Is_Invoked
    {
        public class And_StartTime_Is_Less_Than_ToTime
        {
            [Fact]
            public async Task It_Bulk_Logs_Times()
            {
                var holidayDate = new DateTime(2020, 1, 1);
                var fromDate = new DateTime(2019, 12, 30, 0, 0, 0);
                var toDate = new DateTime(2020, 1, 10, 0, 0, 0);

                var client = new PublicHolidayClientStub()
                    .WithResponse(
                        new PublicHolidayResponseBuilder()
                            .WithSuccess(true)
                            .WithResult(
                                new PublicHolidayResultBuilder()
                                    .WithRecords(
                                        new PublicHolidayRecordBuilder()
                                            .WithDate(holidayDate))));

                var fixture = new TimesheetControllerFixture()
                    .WithPublicHolidayClient(client);

                var result = await fixture.LogBulkTimes(
                    new BulkLogTimesRequestBuilder()
                        .WithOverrideHolidays(null)
                        .WithFromDateTime(fromDate)
                        .WithToDateTime(toDate));

                Assert.Equal(18, fixture.Timesheets.Count());
            }
        }
    }
}