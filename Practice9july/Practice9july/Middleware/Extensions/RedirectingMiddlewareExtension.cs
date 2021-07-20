using Microsoft.AspNetCore.Builder;

namespace Practice9july.Middleware.Extensions
{
    public static class RedirectingMiddlewareExtension
    {
        public static IApplicationBuilder UseRedirecting(this IApplicationBuilder applicationBuilder)
        {
            return applicationBuilder.UseMiddleware<RedirectingMiddleware>();
        }
    }
}
