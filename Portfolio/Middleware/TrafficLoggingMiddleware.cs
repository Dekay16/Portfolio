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
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class TrafficLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly TimeSpan ThrottleTime = TimeSpan.FromHours(1);

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

    public static class TrafficMiddlewareExtensions
    {
        public static IApplicationBuilder UseTrafficLogging(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<TrafficLoggingMiddleware>();
        }
    }
}
