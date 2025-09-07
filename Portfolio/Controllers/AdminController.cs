using Microsoft.AspNetCore.Mvc;
using Portfolio.Business;
using Portfolio.Business.Managers;
using Portfolio.Context.Models;
using System;

namespace Portfolio.Controllers
{
    [Route("admin")]
    public class AdminController : Controller
    {
        private readonly AdminManager _adminManager;
        private readonly IErrorLogger _logger;

        public AdminController(AdminManager manager, IErrorLogger logger)
        {
            _adminManager = manager;
            _logger = logger;
        }
        [HttpGet("Traffic")]
        public IActionResult Traffic()
        {
            return View();
        }

        [HttpGet("Traffic/Summary")]
        public IActionResult GetTrafficSummary(string range, DateTime? startDate, DateTime? endDate)
        {
            var summary = _adminManager.GetTrafficSummary(range, startDate, endDate);
            return Json(summary.Select(s => new { label = s.Date.ToString("g"), count = s.Count }));
        }

        [HttpGet("Traffic/Logs")]
        public IActionResult GetTrafficLogs(DateTime? startDate, DateTime? endDate)
        {
            var logs = _adminManager.GetTrafficLogs(startDate, endDate);

            var shaped = logs.Select(l => new
            {
                l.IpAddress,
                l.UserId,
                l.PathAccessed,
                l.UserAgent,
                l.TimeStamp
            });

            return Json(new { data = shaped });
        }
    }
}

