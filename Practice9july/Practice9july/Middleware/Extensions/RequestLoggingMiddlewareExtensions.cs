using Microsoft.AspNetCore.Builder;

namespace Practice9july.Middleware.Extensions
{
    public static class RequestLoggingMiddlewareExtensions
    {
        public static IApplicationBuilder UseRequestLogging(this IApplicationBuilder applicationBuilder)
        {
            return applicationBuilder.UseMiddleware<RequestLoggingMiddleware>();
        }
    }
}
