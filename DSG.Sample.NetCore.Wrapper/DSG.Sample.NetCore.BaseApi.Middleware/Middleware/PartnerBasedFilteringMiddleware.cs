using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace DSG.Sample.NetCore.BaseApi.Middleware
{
    public class PartnerBasedFilteringMiddleware
    {
        /*
         * Note: This middleware needs to be ran AFTER the RequestPropertiesMiddleware
         * This is because our InvokeAsync needs RequestProperties to get at the Partner info
         *
         * Additionally, this middleware needs to be ran AFTER the EnableRequestRewindMiddleware
         * This is because we read and modify the HttpContext.Request.Body stream
         *
         */

        private readonly RequestDelegate _next;

        public PartnerBasedFilteringMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, RequestProperties requestProperties)
        {
            await FilterRequestProperties(context);

            // Call the next delegate/middleware in the pipeline
            await _next(context);

            await FilterResponseProperties(context);
        }

        private async Task FilterRequestProperties(HttpContext context)
        {
            if (context.Request.ContentLength.GetValueOrDefault() == 0)
                return;

            var requestJson = await ReadBodyAsString(context.Request);
            TrimUnauthorizedProperties(requestJson);
            ReplaceRequestBody(context.Request, requestJson);
        }

        private async Task FilterResponseProperties(HttpContext context)
        {
            // TODO
            await Task.CompletedTask;
        }

        private async Task<JObject> ReadBodyAsString(HttpRequest request)
        {
            try
            {
                var reader = new StreamReader(request.Body);
                var requestBody = await reader.ReadToEndAsync();

                return JObject.Parse(requestBody);
            }
            catch (JsonReaderException)
            {
                // TODO: Add message specific to bad JSON being provided
                throw;
            }
            finally
            {
                // Reset the body position, just in case something goes wrong.
                // That way, subsequent middleware/controllers can still read the body.
                // Note: This requires the EnableRequestRewindMiddleware
                request.Body.Position = 0;
            }
        }

        private void TrimUnauthorizedProperties(JObject requestJson)
        {
            // TODO: Replace this with actual property pruning code
            var allowedProperties = new[] { "id", "text" };
            var requestKeys = (((IDictionary<string, JToken>)requestJson).Keys).ToArray();

            foreach (var key in requestKeys)
            {
                if (allowedProperties.Any(x => x == key.ToLower()))
                    continue;

                requestJson.Remove(key);
            }
        }

        private void ReplaceRequestBody(HttpRequest request, JObject requestJson)
        {
            var jsonString = JsonConvert.SerializeObject(requestJson);
            var newBody = Encoding.UTF8.GetBytes(jsonString);
            request.Body = new MemoryStream(newBody);
        }
    }

    public static class PartnerBasedFilteringMiddlewareExtensions
    {
        public static IApplicationBuilder UsePartnerBasedFiltering(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<PartnerBasedFilteringMiddleware>();
        }
    }
}