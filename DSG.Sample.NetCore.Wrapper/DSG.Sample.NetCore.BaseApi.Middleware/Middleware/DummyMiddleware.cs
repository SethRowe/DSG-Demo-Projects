using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;

namespace DSG.Sample.NetCore.BaseApi.Middleware
{
    public class DummyMiddleware
    {
        /*
         * This 'dummy' middleware doesn't actually do anything, but is very useful
         * as a "hello world" example, or during debugging of a middleware pipeline.
         */

        private readonly RequestDelegate _next;

        public DummyMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Call the next delegate/middleware in the pipeline
            await _next(context);
        }
    }

    public static class DummyMiddlewareExtensions
    {
        public static IApplicationBuilder UseDummyMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<DummyMiddleware>();
        }
    }
}