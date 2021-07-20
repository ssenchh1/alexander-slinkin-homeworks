using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Practice9july.Middleware
{
    public class RedirectingMiddleware
    {
        private readonly RequestDelegate _next;

        public RedirectingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            if (!string.IsNullOrEmpty(httpContext.Request.Query["r"].ToString()))
            {
                httpContext.Response.Redirect(httpContext.Request.Query["r"]);
            }
            await _next(httpContext);
        }
    }
}
