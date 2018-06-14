using System;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using DSG.Sample.NetCore.BaseApi.Middleware.ActionFilters;
using Microsoft.AspNetCore.Builder;
using Newtonsoft.Json;

namespace DSG.Sample.NetCore.BaseApi.Middleware
{
    public class FailureResultRewritingMiddleware
    {
        private readonly RequestDelegate _next;

        public FailureResultRewritingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, RequestProperties requestProperties)
        {
            await _next(context);

            await HandleFailureResult(context);
        }

        private async Task HandleFailureResult(HttpContext context)
        {
            var response = context.Response;
            
            // TODO: Better refine how we determine if we're in a 'Failure' response
            if (response.StatusCode < 405)
                return;

            context.Response.ContentType = "application/json";

            var result = JsonConvert.SerializeObject(GetFailureResponse(context));
            await response.WriteAsync(result);
        }

        private FailureResponse GetFailureResponse(HttpContext context)
        {
            var failureResponse = new FailureResponse
            {
                Id = 12312312312 // TODO: set these properties correctly
            };

            // TODO: If we don't find the key, use the RouteData.ActionName and append ".Failure"?

            if (!context.Items.TryGetValue(ResponseMessagesAttribute.FailureMessageKey, out var exceptionMessageObj))
                return failureResponse;

            // TODO: Call the 'GetMessageContent' or whatever to get the real message

            var failureMessage = Convert.ToString(exceptionMessageObj);

            failureResponse.DeveloperMessage = $"Sample developer message for key '{failureMessage}'";
            failureResponse.UserMessage = $"Sample user message for key '{failureMessage}'";

            return failureResponse;
        }
    }

    public static class FailureResultRewritingMiddlewareExtensions
    {
        public static IApplicationBuilder UseFailureResultRewriting(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<FailureResultRewritingMiddleware>();
        }
    }
}