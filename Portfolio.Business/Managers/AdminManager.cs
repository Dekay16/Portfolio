using Microsoft.EntityFrameworkCore;
using Portfolio.Business.ViewModels;
using Portfolio.Context.Models;
using Portfolio.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Portfolio.Business.Managers
{
    public class AdminManager
    {
        private readonly ApplicationDbContext _db;
        private readonly IErrorLogger _logger;

        public AdminManager(ApplicationDbContext db, IErrorLogger logger)
        {
            _db = db;
            _logger = logger;
        }

        #region Traffic
        public List<TrafficSummaryViewModel> GetTrafficSummary(string range, DateTime? startDate, DateTime? endDate)
        {
            var query = _db.TrafficLog.AsQueryable();
            DateTime now = DateTime.Now;

            if (range == "day")
            {
                var day = now.Date;
                var grouped = query
                    .Where(t => t.TimeStamp.Date == day)
                    .GroupBy(t => t.TimeStamp.Hour)
                    .Select(g => new { Hour = g.Key, Count = g.Count() })
                    .ToList();

                // Fill all 24 hours
                var result = new List<TrafficSummaryViewModel>();
                for (int h = 0; h < 24; h++)
                {
                    var data = grouped.FirstOrDefault(g => g.Hour == h);
                    result.Add(new TrafficSummaryViewModel
                    {
                        Date = day.AddHours(h),
                        Count = data?.Count ?? 0
                    });
                }
                return result;
            }
            else if (range == "week")
            {
                var start = now.Date.AddDays(-6); // past 7 days including today
                var grouped = query
                    .Where(t => t.TimeStamp.Date >= start)
                    .GroupBy(t => t.TimeStamp.Date)
                    .Select(g => new { Date = g.Key, Count = g.Count() })
                    .ToList();

                var result = new List<TrafficSummaryViewModel>();
                for (int i = 0; i < 7; i++)
                {
                    var date = start.AddDays(i);
                    var data = grouped.FirstOrDefault(g => g.Date == date);
                    result.Add(new TrafficSummaryViewModel
                    {
                        Date = date,
                        Count = data?.Count ?? 0
                    });
                }
                return result;
            }
            else if (range == "month")
            {
                var start = now.Date.AddDays(-29); // past 30 days including today
                var grouped = query
                    .Where(t => t.TimeStamp.Date >= start)
                    .GroupBy(t => t.TimeStamp.Date)
                    .Select(g => new { Date = g.Key, Count = g.Count() })
                    .ToList();

                var result = new List<TrafficSummaryViewModel>();
                for (int i = 0; i < 30; i++)
                {
                    var date = start.AddDays(i);
                    var data = grouped.FirstOrDefault(g => g.Date == date);
                    result.Add(new TrafficSummaryViewModel
                    {
                        Date = date,
                        Count = data?.Count ?? 0
                    });
                }
                return result;
            }
            else if (range == "custom" && startDate.HasValue && endDate.HasValue)
            {
                var totalDays = (endDate.Value.Date - startDate.Value.Date).Days + 1;
                var grouped = query
                    .Where(t => t.TimeStamp.Date >= startDate.Value.Date && t.TimeStamp.Date <= endDate.Value.Date)
                    .GroupBy(t => t.TimeStamp.Date)
                    .Select(g => new { Date = g.Key, Count = g.Count() })
                    .ToList();

                var result = new List<TrafficSummaryViewModel>();
                for (int i = 0; i < totalDays; i++)
                {
                    var date = startDate.Value.Date.AddDays(i);
                    var data = grouped.FirstOrDefault(g => g.Date == date);
                    result.Add(new TrafficSummaryViewModel
                    {
                        Date = date,
                        Count = data?.Count ?? 0
                    });
                }
                return result;
            }

            return new List<TrafficSummaryViewModel>();
        }

        public List<TrafficLog> GetTrafficLogs(DateTime? startDate, DateTime? endDate)
        {
            var query = _db.TrafficLog.AsQueryable();

            if (startDate.HasValue && endDate.HasValue)
                query = query.Where(t => t.TimeStamp >= startDate && t.TimeStamp <= endDate);

            return query
                .OrderByDescending(l => l.TimeStamp)
                .ToList();
        }

    }
    #endregion

}
