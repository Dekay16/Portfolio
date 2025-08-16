using Microsoft.AspNetCore.Mvc;
using Portfolio.Business.Managers;
using System;

namespace Portfolio.Controllers
{
    [Route("admin")]
    public class AdminController : Controller
    {
        private readonly AdminManager _manager;

        public AdminController(AdminManager manager)
        {
            _manager = manager;
        }

        [HttpGet("VisitsPerDay")] // action-level route
        public IActionResult VisitsPerDay(string range = "day", string date = null)
        {
            DateTime? selectedDay = null;
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
    }
}

