using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Portfolio.Business.ViewModels;
using Portfolio.Context.Models;
using Portfolio.Data;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Business.Managers
{
    public class AdminManager
    {
        private readonly ApplicationDbContext _db;

        public AdminManager(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task AddLogAsync(string ip, string path)
        {
            var lastLog = await _db.TrafficLog
                .Where(t => t.IpAddress == ip && t.PathAccessed == path)
                .OrderByDescending(t => t.TimeStamp)
                .FirstOrDefaultAsync();

            if (lastLog == null || lastLog.TimeStamp < DateTime.Now.AddHours(-1))
            {
                var log = new TrafficLog
                {
                    IpAddress = ip,
                    PathAccessed = path,
                    TimeStamp = DateTime.Now
                };
                _db.TrafficLog.Add(log);
                await _db.SaveChangesAsync();
            }
        }

        public List<TrafficSummaryViewModel> GetTraffic(string range, DateTime? selectedDay = null)
        {
            var now = DateTime.UtcNow;
            IQueryable<TrafficLog> query = _db.TrafficLog;

            if (range == "day")
            {
                // If a day is selected, use it; otherwise default to today
                var day = selectedDay?.Date ?? now.Date;

                // Group by hour
                var grouped = query
                    .Where(t => t.TimeStamp.Date == day)
                    .GroupBy(t => t.TimeStamp.Hour)
                    .Select(g => new { Hour = g.Key, Count = g.Count() })
                    .AsEnumerable() // materialize for in-memory projection
                    .Select(x => new TrafficSummaryViewModel
                    {
                        Date = new DateTime(day.Year, day.Month, day.Day, x.Hour, 0, 0),
                        Count = x.Count
                    })
                    .OrderBy(x => x.Date)
                    .ToList();

                // Fill missing hours with 0
                var result = new List<TrafficSummaryViewModel>();
                for (int h = 0; h < 24; h++)
                {
                    var hourData = grouped.FirstOrDefault(x => x.Date.Hour == h);
                    result.Add(hourData ?? new TrafficSummaryViewModel { Date = new DateTime(day.Year, day.Month, day.Day, h, 0, 0), Count = 0 });
                }

                return result;
            }

            if (range == "week")
            {
                var start = now.AddDays(-7);
                return query
                    .Where(t => t.TimeStamp >= start)
                    .GroupBy(t => t.TimeStamp.Date)
                    .Select(g => new TrafficSummaryViewModel { Date = g.Key, Count = g.Count() })
                    .OrderBy(x => x.Date)
                    .ToList();
            }

            if (range == "month")
            {
                var start = now.AddMonths(-1);
                return query
                    .Where(t => t.TimeStamp >= start)
                    .GroupBy(t => t.TimeStamp.Date)
                    .Select(g => new TrafficSummaryViewModel { Date = g.Key, Count = g.Count() })
                    .OrderBy(x => x.Date)
                    .ToList();
            }

            return new List<TrafficSummaryViewModel>();
        }
    }
}