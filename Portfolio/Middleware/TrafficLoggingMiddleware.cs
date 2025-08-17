using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using NuGet.Protocol.Core.Types;
using Portfolio.Business.Managers;
using Portfolio.Context.Models;
using Portfolio.Data;
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

        public async Task Invoke(HttpContext httpContext, AdminManager manager)
        {
            var ip = httpContext.Connection.RemoteIpAddress?.ToString() ?? "unknown";
            var path = httpContext.Request.Path.ToString();

            await manager.AddLogAsync(ip, path);

            await _next(httpContext);

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
