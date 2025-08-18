using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
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

        public TrafficLoggingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, ApplicationDbContext db)
        {
            var path = context.Request.Path.Value;

            if (!IsStaticFile(path))
            {
                var log = new TrafficLog
                {
                    PathAccessed = path,
                    UserId = context.User.Identity?.Name ?? "Anonymous",
                    TimeStamp = DateTime.Now,
                    IpAddress = GetIpAddress(context),
                };

                db.TrafficLog.Add(log);
                await db.SaveChangesAsync();
            }

            await _next(context);
        }

        private bool IsStaticFile(string path)
        {
            if (string.IsNullOrEmpty(path))
                return true;

            // Exclude files with common extensions
            string[] staticFileExtensions = new[]
            {
            ".css", ".js", ".png", ".jpg", ".jpeg", ".gif", ".ico", ".svg",
            ".woff", ".woff2", ".ttf", ".map"
        };

            return staticFileExtensions.Any(ext => path.EndsWith(ext, StringComparison.OrdinalIgnoreCase));
        }
        private string GetIpAddress(HttpContext context)
        {
            // If you're behind a proxy/load balancer, look at X-Forwarded-For first
            var ip = context.Request.Headers["X-Forwarded-For"].FirstOrDefault();

            if (string.IsNullOrEmpty(ip))
            {
                ip = context.Connection.RemoteIpAddress?.ToString();
            }

            return ip ?? "Unknown";
        }
    }


    /// <summary>
    /// Allows for some cleaner code in Program.cs file. Can call this directly instead of app.UseMiddleware<>();
    /// </summary>
    public static class TrafficMiddlewareExtensions
    {
        public static IApplicationBuilder UseTrafficLogging(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<TrafficLoggingMiddleware>();
        }
    }
}
