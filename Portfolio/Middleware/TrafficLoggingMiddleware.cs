using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using NuGet.Protocol.Core.Types;
using Portfolio.Business.Managers;
using Portfolio.Context.Models;
using Portfolio.Data;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Portfolio.Middleware
{
    public class TrafficLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IMemoryCache _cache;

        public TrafficLoggingMiddleware(RequestDelegate next, IMemoryCache cache)
        {
            _next = next;
            _cache = cache;
        }

        public async Task InvokeAsync(HttpContext context, ApplicationDbContext db)
        {
            var path = context.Request.Path.Value;
            if (IsStaticFile(path))
            {
                await _next(context);
                return;
            }

            var userId = context.User.Identity?.Name ?? "Anonymous";
            var ip = GetIpAddress(context);
            var key = $"{userId}_{ip}";

            var now = DateTime.Now;

            if (_cache.TryGetValue<(string Path, DateTime TimeStamp)>(key, out var lastVisit))
            {
                bool pageChanged = lastVisit.Path != path;
                bool overHour = (now - lastVisit.TimeStamp) > TimeSpan.FromHours(1);

                if (pageChanged || overHour)
                {
                    await SaveLogAsync(db, path, userId, ip);

                    // update cache with sliding + absolute expiration
                    _cache.Set(key, (path, now), new MemoryCacheEntryOptions
                    {
                        SlidingExpiration = TimeSpan.FromHours(1),   // if not used for 1h, remove
                        AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(24) // hard cap
                    });
                }
            }
            else
            {
                // first visit for this user/IP
                await SaveLogAsync(db, path, userId, ip);

                _cache.Set(key, (path, now), new MemoryCacheEntryOptions
                {
                    SlidingExpiration = TimeSpan.FromHours(1),
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(24)
                });
            }

            await _next(context);
        }

        private async Task SaveLogAsync(ApplicationDbContext db, string path, string userId, string ip)
        {
            var log = new TrafficLog
            {
                PathAccessed = path,
                UserId = userId,
                TimeStamp = DateTime.Now,
                IpAddress = ip,
            };

            db.TrafficLog.Add(log);
            await db.SaveChangesAsync();
        }

        private bool IsStaticFile(string path)
        {
            if (string.IsNullOrEmpty(path))
                return true;

            string[] staticFileExtensions = new[]
            {
                ".css", ".js", ".png", ".jpg", ".jpeg", ".gif", ".ico", ".svg",
                ".woff", ".woff2", ".ttf", ".map"
            };

            return staticFileExtensions.Any(ext =>
                path.EndsWith(ext, StringComparison.OrdinalIgnoreCase));
        }

        private string GetIpAddress(HttpContext context)
        {
            var ip = context.Request.Headers["X-Forwarded-For"].FirstOrDefault();

            if (string.IsNullOrEmpty(ip))
            {
                ip = context.Connection.RemoteIpAddress?.ToString();
            }

            return ip ?? "Unknown";
        }
    }

    public static class TrafficMiddlewareExtensions
    {
        public static IApplicationBuilder UseTrafficLogging(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<TrafficLoggingMiddleware>();
        }
    }
}
