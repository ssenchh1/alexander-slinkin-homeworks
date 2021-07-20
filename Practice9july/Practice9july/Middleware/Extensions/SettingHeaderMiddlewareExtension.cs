using Microsoft.AspNetCore.Builder;

namespace Practice9july.Middleware.Extensions
{
    public static class SettingHeaderMiddlewareExtension
    {
        public static IApplicationBuilder UseSettingHeader(this IApplicationBuilder applicationBuilder)
        {
            return applicationBuilder.UseMiddleware<SettingHeaderMiddleware>();
        }
    }
}
