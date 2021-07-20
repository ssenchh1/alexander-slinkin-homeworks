using Microsoft.AspNetCore.Builder;

namespace Practice9july.Middleware.Extensions
{
    public static class ExceptionLoggingMiddlewareExtention
    {
        public static IApplicationBuilder UseExceptionLogging(this IApplicationBuilder applicationBuilder)
        {
            return applicationBuilder.UseMiddleware<ExceptionLoggingMiddleware>();
        }
    }
}
