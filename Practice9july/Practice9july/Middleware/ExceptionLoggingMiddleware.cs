using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.Extensions.Logging;

namespace Practice9july.Middleware
{
    public class ExceptionLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public ExceptionLoggingMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
        {
            var path = Directory.GetCurrentDirectory();
            _logger = new LoggerFactory().AddFile($"{path}//Logs//Log.txt").CreateLogger<RequestLoggingMiddleware>();
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message + e.StackTrace + httpContext.Request.GetDisplayUrl());
            }
        }
    }
}
