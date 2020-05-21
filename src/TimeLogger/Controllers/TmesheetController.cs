﻿using Microsoft.AspNetCore.Mvc;
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

        [HttpGet]
        public IActionResult GetTimeSheets([FromQuery]DateTime? startDate, [FromQuery] DateTime? endDate)
        {
            var timeSheets = _timeLoggerService.GetTimesheet(
                startDate ?? DateTime.Now.ToLocalTime().AddDays(-7),
                endDate ?? DateTime.Now.ToLocalTime().AddDays(1));

            return Ok(timeSheets);
        }

        [HttpPost("in")]
        public IActionResult PostInTime([FromBody] LogTimeRequest request)
        {
            if (request == null || request?.LogTime == null)
            {
                request.LogTime = DateTime.Now.ToLocalTime();
            }

            _timeLoggerService.LogInTime(request);

            return Ok();
        }

        [HttpPost("in")]
        public IActionResult PostOutTime([FromBody] LogTimeRequest request)
        {
            if (request == null || request?.LogTime == null)
            {
                request.LogTime = DateTime.Now.ToLocalTime();
            }

            _timeLoggerService.LogOutTime(request);

            return Ok();
        }

        [HttpPost("{id}")]
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