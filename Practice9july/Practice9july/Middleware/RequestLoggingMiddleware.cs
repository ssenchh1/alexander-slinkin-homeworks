using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.Extensions.Logging;

namespace Practice9july.Middleware
{
    public class RequestLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public RequestLoggingMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<RequestLoggingMiddleware>();
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            _logger.LogWarning("Request path: " + httpContext.Request.GetDisplayUrl());
            await _next(httpContext);
        }
    }
}
