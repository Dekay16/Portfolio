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
        private readonly AdminManager _manager;
        private readonly IErrorLogger _logger;

        public AdminController(AdminManager manager, IErrorLogger logger)
        {
            _manager = manager;
            _logger = logger;
        }

        /// <summary>
        /// Used to populate the chart
        /// </summary>
        /// <param name="range"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        [HttpGet("VisitsPerDay")]
        public IActionResult VisitsPerDay(string range = "day", string date = null)
        {
            DateTime? selectedDay = null;
            try
            {
                if (DateTime.TryParse(date, out var parsedDate))
                    selectedDay = parsedDate;

                var data = _manager.GetTraffic(range, selectedDay)
                    .Select(x => new { Label = x.Date.ToString(range == "day" ? "HH:mm" : "MM-dd"), Count = x.Count })
                    .ToList();

                ViewBag.Range = range;
                ViewBag.SelectedDay = selectedDay?.ToString("yyyy-MM-dd") ?? DateTime.UtcNow.ToString("yyyy-MM-dd");
                ViewBag.Data = System.Text.Json.JsonSerializer.Serialize(data);

                return View();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in AdminController.VisitsPerDay");
                throw;
            }
        }

        /// <summary>
        /// Used to populate the table with json
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetTrafficTable")]
        public IActionResult GetTrafficTable()
        {
            try
            {
                List<TrafficLog> data = new List<TrafficLog>();

                data = _manager.GetTrafficTable();

                return Json(new { data });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in AdminController.GetTrafficTable");
                throw;
            }
        }
    }
}

