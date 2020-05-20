using Microsoft.AspNetCore.Mvc;
using System;
using TimeLogger.Models;
using TimeLogger.Services;

namespace TimeLogger.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TmesheetController : ControllerBase
    {
        private readonly ITimeLoggerService _timeLoggerService;

        public TmesheetController(ITimeLoggerService timeLoggerService)
        {
            _timeLoggerService = timeLoggerService;
        }

        [HttpPost("in")]
        public IActionResult PostInTime([FromBody] LogTimeRequest request)
        {
            if (request.LogTime == null)
            {
                request.LogTime = DateTime.Now.ToLocalTime();
            }

            _timeLoggerService.LogInTime(request);

            return Ok();
        }

        [HttpPost("in")]
        public IActionResult PostOutTime([FromBody] LogTimeRequest request)
        {
            if (request.LogTime == null)
            {
                request.LogTime = DateTime.Now.ToLocalTime();
            }

            _timeLoggerService.LogOutTime(request);

            return Ok();
        }
    }
}