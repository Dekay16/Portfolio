using Microsoft.AspNetCore.Mvc;
using Portfolio.Business.Interfaces;
using Portfolio.Business.Managers;
using Portfolio.Context.Models;
using System;

namespace Portfolio.Controllers
{
    [Route("Admin")]
    public class AdminController : Controller
    {
        private readonly IAdminManager _adminManager;
        private readonly IErrorLogger _logger;

        public AdminController(IAdminManager manager, IErrorLogger logger)
        {
            _adminManager = manager;
            _logger = logger;
        }

        #region Traffic

        [HttpGet("Traffic")]
        public IActionResult Traffic()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in AdminController.Traffic");
                return StatusCode(500);
            }
        }

        [HttpGet("Traffic/Summary")]
        public IActionResult GetTrafficSummary(string range, DateTime? startDate, DateTime? endDate)
        {
            try
            {
                var summary = _adminManager.GetTrafficSummary(range, startDate, endDate);
                return Json(summary.Select(s => new { label = s.Date.ToString("g"), count = s.Count }));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in AdminController.GetTrafficSummary");
                return StatusCode(500);
            }
        }

        [HttpGet("Traffic/Logs")]
        public IActionResult GetTrafficLogs(DateTime? startDate, DateTime? endDate)
        {
            try
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
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in AdminController.GetTrafficLogs");
                return Json(new { data = Array.Empty<object>() });
            }
        }

        #endregion

        #region Error Logs (new)

        [HttpGet("Errors")]
        public IActionResult Errors()
        {
            try
            {
                return View(); // Views/Admin/Errors.cshtml
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in AdminController.Errors");
                return StatusCode(500);
            }
        }

        [HttpGet("Errors/Logs")]
        public IActionResult GetErrorLogs(DateTime? startDate, DateTime? endDate)
        {
            try
            {
                var logs = _adminManager.GetErrorLogs(startDate, endDate);

                var shaped = logs.Select(l => new
                {
                    l.ID,
                    l.TimeStamp,
                    l.Level,
                    l.Message,
                    l.Path,
                    l.UserId,
                    l.Type
                }).OrderByDescending(l => l.TimeStamp);

                return Json(new { data = shaped });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in AdminController.GetErrorLogs");
                return Json(new { data = Array.Empty<object>() });
            }
        }

        [HttpGet("Errors/Summary")]
        public IActionResult GetErrorSummary(DateTime? startDate, DateTime? endDate)
        {
            try
            {
                var summary = _adminManager.GetErrorSummary(startDate, endDate);
                // Return label / count pairs for charts
                return Json(summary.Select(s => new { label = s.Date.ToString("d"), count = s.Count }));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in AdminController.GetErrorSummary");
                return StatusCode(500);
            }
        }

        #endregion
    }
}

