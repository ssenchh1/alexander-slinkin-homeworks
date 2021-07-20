using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace Practice9july.Middleware
{
    public class SettingHeaderMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IConfiguration _configuration;

        public SettingHeaderMiddleware(RequestDelegate next, IConfiguration configuration)
        {
            _configuration = configuration;
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            httpContext.Response.Headers.Add("X-Checked-By", _configuration["Surname"]);
            await _next(httpContext);
        }
    }
}
