using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Primitives;

namespace DSG.Sample.NetCore.BaseApi.Middleware
{
    public class RequestPropertiesMiddleware
    {
        private readonly RequestDelegate _next;

        public RequestPropertiesMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, RequestProperties requestProperties)
        {
            SetRequestProperties(requestProperties);

            context.Response.OnStarting(async () => { await SetResponseHeaders(context.Response); });

            await _next(context);
        }

        private void SetRequestProperties(RequestProperties requestProperties)
        {
            requestProperties.Partner = GetApiPartner();
            requestProperties.AuditName = "api:" + requestProperties.Partner.PartnerId.ToString();

            // todo: add the other properties
        }

        private Task SetResponseHeaders(HttpResponse response)
        {
            // TODO: Determine what to do if the response has already started (since we won't be able to write any headers)

            response.Headers.Add("Sample-Header",new StringValues("Sample-Header-Value"));

            // TODO: Add any headers that actually are needed

            return Task.CompletedTask;
        }

        private ApiPartner GetApiPartner()
        {
            // TODO: This should be going to BaseApi, not our fake class

            return new ApiPartner
            {
                PartnerId = 123
            };
        }
    }

    public static class RequestPropertiesMiddlewareExtensions
    {
        public static IApplicationBuilder UseRequestProperties(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<RequestPropertiesMiddleware>();
        }
    }
}