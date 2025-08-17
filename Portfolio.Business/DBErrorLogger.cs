using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Portfolio.Context.Models;
using Portfolio.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Business
{
    public interface IErrorLogger
    {
        void LogError(Exception ex, string message = null);
        void LogInfo(string message);
        void LogWarning(string message);
    }
    public class DBErrorLogger : IErrorLogger
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public DBErrorLogger(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public void LogError(Exception ex, string message = null)
        {
            SaveLog("Error", message ?? ex.Message, ex);
        }

        public void LogInfo(string message)
        {
            SaveLog("Info", message);
        }

        public void LogWarning(string message)
        {
            SaveLog("Warning", message);
        }

        private void SaveLog(string level, string message, Exception ex = null)
        {
            var httpContext = _httpContextAccessor.HttpContext;

            var log = new ErrorLog
            {
                TimeStamp = DateTime.UtcNow,
                Level = level,
                Message = message,
                StackTrace = ex?.StackTrace,
                Source = ex?.Source,
                Path = httpContext?.Request.Path,
                UserId = httpContext?.User?.Identity?.Name,
                Type = ex?.GetType().ToString()
            };

            _context.Errors.Add(log);
            _context.SaveChanges();
        }
    }
}
