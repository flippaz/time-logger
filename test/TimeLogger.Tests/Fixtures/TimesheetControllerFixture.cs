using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TimeLogger.Clients;
using TimeLogger.Controllers;
using TimeLogger.Models;
using TimeLogger.Repository.Context;
using TimeLogger.Repository.Entities;
using TimeLogger.Tests.Framework;

namespace TimeLogger.Tests.Fixtures
{
    public class TimesheetControllerFixture
    {
        private Action<IServiceCollection> _setup;

        public TimesheetControllerFixture()
        {
            _setup = services => _ = services.AddControllers();

            Timesheets = new List<Timesheet>();
        }

        public List<Timesheet> Timesheets { get; set; }

        public async Task<IActionResult> LogBulkTimes(BulkLogTimesRequest request)
        {
            IServiceProvider provider = TestServiceProvider.CreateProvider(
                services => _setup(services));

            using IServiceScope scope = provider.CreateScope();

            var result = await scope.ServiceProvider
                .GetRequiredService<TimesheetController>()
                .LogBulkTimes(request);

            TimeLoggerContext dbContext = scope.ServiceProvider.GetRequiredService<TimeLoggerContext>();

            Timesheets.AddRange(dbContext.TimeSheets);
            return result;
        }

        public async Task<IActionResult> PostInTime(LogTimeRequest request)
        {
            IServiceProvider provider = TestServiceProvider.CreateProvider(
                services => _setup(services));

            using IServiceScope scope = provider.CreateScope();

            var result = await scope.ServiceProvider
                .GetRequiredService<TimesheetController>()
                .PostInTime(request);

            TimeLoggerContext dbContext = scope.ServiceProvider.GetRequiredService<TimeLoggerContext>();

            Timesheets.AddRange(dbContext.TimeSheets);
            return result;
        }

        public TimesheetControllerFixture WithPublicHolidayClient(IPublicHolidayClient publicHolidayClient)
        {
            _setup += services => services.Replace(ServiceDescriptor.Singleton(publicHolidayClient));

            return this;
        }
    }
}