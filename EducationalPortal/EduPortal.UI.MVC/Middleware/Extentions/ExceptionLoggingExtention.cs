using Microsoft.AspNetCore.Builder;

namespace EduPortal.UI.MVC.Middleware.Extentions
{
    public static class ExceptionLoggingExtention
    {
        public static IApplicationBuilder UseExceptionLogging(this IApplicationBuilder applicationBuilder)
        {
            return applicationBuilder.UseMiddleware<ExceptionLoggingMiddleware>();
        }
    }
}
