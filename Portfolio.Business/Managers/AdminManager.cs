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
        private readonly IErrorLogger _logger;

        public AdminManager(ApplicationDbContext db, IErrorLogger logger)
        {
            _db = db;
            _logger = logger;
        }

        #region Traffic
        /// <summary>
        /// Used to populate the chart
        /// </summary>
        /// <param name="range"></param>
        /// <param name="selectedDay"></param>
        /// <returns></returns>
        public List<TrafficSummaryViewModel> GetTraffic(string range, DateTime? selectedDay = null)
        {
            try
            {
                var now = DateTime.Now;
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
                        .AsEnumerable() 
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
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in Admin Manager: GetTraffic");
                throw;
            }
        }

        /// <summary>
        /// Used to populate the table
        /// </summary>
        /// <returns></returns>
        public List<TrafficLog> GetTrafficTable()
        {
            try
            {
                return _db.TrafficLog.OrderBy(x => x.ID).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in Admin Manager: GetTrafficTable");
                throw;
            }
        }
        #endregion
    }
}