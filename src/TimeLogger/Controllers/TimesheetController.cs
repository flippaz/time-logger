using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeLogger.Models;
using TimeLogger.Services;

namespace TimeLogger.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TimesheetController : ControllerBase
    {
        private readonly ITimeLoggerService _timeLoggerService;

        public TimesheetController(ITimeLoggerService timeLoggerService)
        {
            _timeLoggerService = timeLoggerService;
        }

        [HttpPost("helper/bulk")]
        public async Task<IActionResult> BulkLogTimes([FromBody] IList<LogTimeRequest> request)
        {
            if (request == null || request.Any(l => l.LogTime == null) || request.Any(l => l.LogAction == null))
            {
                return new JsonResult(new { message = "Invalid request" });
            }

            await _timeLoggerService.BulkLogTimes(request);

            return Ok();
        }

        [HttpDelete("delete/{id}")]
        public IActionResult DeleteTime(int id)
        {
            _timeLoggerService.DeleteTime(id);

            return Ok();
        }

        [HttpGet]
        public IActionResult GetTimeSheets([FromQuery] DateTime? startDate, [FromQuery] DateTime? endDate)
        {
            var timeSheets = _timeLoggerService.GetTimesheet(
                startDate ?? DateTime.Now.ToLocalTime().AddDays(-7),
                endDate ?? DateTime.Now.ToLocalTime().AddDays(1));

            return Ok(timeSheets);
        }

        [HttpPost("in")]
        public async Task<IActionResult> PostInTime([FromBody] LogTimeRequest request)
        {
            if (request == null || request?.LogTime == null)
            {
                request.LogTime = DateTime.Now.ToLocalTime();
            }

            await _timeLoggerService.LogInTime(request);

            return Ok();
        }

        [HttpPost("out")]
        public async Task<IActionResult> PostOutTime([FromBody] LogTimeRequest request)
        {
            if (request == null || request?.LogTime == null)
            {
                request.LogTime = DateTime.Now.ToLocalTime();
            }

            await _timeLoggerService.LogOutTime(request);

            return Ok();
        }

        [HttpPost("update/{id}")]
        public IActionResult PostUpdateTime(int id, [FromBody] LogTimeRequest request)
        {
            if (request == null || request?.LogTime == null)
            {
                request.LogTime = DateTime.Now.ToLocalTime();
            }

            _timeLoggerService.UpdateTime(id, request);

            return Ok();
        }
    }
}