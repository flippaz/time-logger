using System;
using System.Threading.Tasks;
using TimeLogger.Tests.Doubles;
using TimeLogger.Tests.Fixtures;
using TimeLogger.Tests.Framework.Builders;
using Xunit;

namespace TimeLogger.Tests
{
    public class When_PostInTime_Endpoint_Is_Invoked
    {
        public class And_Log_In_Time_Is_Not_A_Holiday
        {
            [Fact]
            public async Task It_Logs_In_Time()
            {
                var holidayDate = new DateTime(2020, 4, 1);
                var logDate = DateTime.Now.ToLocalTime();

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

                var result = await fixture.PostInTime(new LogTimeRequestBuilder().WithLogTime(logDate));

                Assert.Single(fixture.Timesheets);
            }
        }

        public class And_Log_In_Time_Is_On_A_Holiday_Or_Weekend
        {
            [Theory]
            [InlineData("Jan 1, 2020", "Jan 1, 2020")]
            [InlineData("Jan 1, 2020", "Jan 4, 2020")] //Saturday
            [InlineData("Jan 1, 2020", "Jan 5, 2020")] //Sunday
            public async Task It_Does_Not_Log_In_Time(string holidayDateString, string logDateString)
            {
                var holidayDate = DateTime.Parse(holidayDateString);
                var logDate = DateTime.Parse(logDateString);

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

                var result = await fixture.PostInTime(
                    new LogTimeRequestBuilder()
                        .WithLogTime(logDate)
                        .WithOverrideHolidays(null));

                Assert.Empty(fixture.Timesheets);
            }
        }

        public class And_Log_In_Time_Override_Holiday_Value_Is_True
        {
            [Theory]
            [InlineData("Jan 1, 2020", "Jan 1, 2020")]
            [InlineData("Jan 1, 2020", "Jan 4, 2020")] //Saturday
            [InlineData("Jan 1, 2020", "Jan 5, 2020")] //Sunday
            public async Task It_Logs_In_Time(string holidayDateString, string logDateString)
            {
                var holidayDate = DateTime.Parse(holidayDateString);
                var logDate = DateTime.Parse(logDateString);

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

                var result = await fixture.PostInTime(
                    new LogTimeRequestBuilder()
                        .WithOverrideHolidays(true)
                        .WithLogTime(logDate));

                Assert.Single(fixture.Timesheets);
            }
        }
    }
}