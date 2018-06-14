using System;
using System.Threading.Tasks;
using DSG.Sample.NetCore.BaseApi.Middleware.ActionFilters;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace DSG.Sample.NetCore.BaseApi.Middleware
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context /* other dependencies */)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            // TODO: Log the exception
            // logger.Log(ex);

            // If the response has already started, we can't change it now.
            // Normally this means something broke AFTER the controller finished running (i.e. another middleware)
            if (context.Response.HasStarted)
                return;

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = 500; // TODO: Add logic for changing StatusCode based on what went wrong?

            var result = JsonConvert.SerializeObject(GetExceptionResponse(context));
            await context.Response.WriteAsync(result);
        }

        private FailureResponse GetExceptionResponse(HttpContext context)
        {
            var failureResponse = new FailureResponse
            {
                Id = 12312312312 // TODO: set these properties correctly
            };

            // TODO: If we don't find the key, use the RouteData.ActionName and append ".Exception"?

            if (!context.Items.TryGetValue(ResponseMessagesAttribute.ExceptionMessageKey, out var exceptionMessageObj))
                return failureResponse;

            // TODO: Call the 'GetMessageContent' or whatever to get the real message

            var exceptionMessage = Convert.ToString(exceptionMessageObj);

            failureResponse.DeveloperMessage = $"Sample developer message for key '{exceptionMessage}'";
            failureResponse.UserMessage = $"Sample user message for key '{exceptionMessage}'";

            return failureResponse;
        }
    }

    public static class ErrorHandlingMiddlewareExtensions
    {
        public static IApplicationBuilder UseErrorHandling(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ErrorHandlingMiddleware>();
        }
    }
}