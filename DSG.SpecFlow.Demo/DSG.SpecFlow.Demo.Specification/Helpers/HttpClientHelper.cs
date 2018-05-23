using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace DSG.SpecFlow.Demo.Specification.Helpers
{
    public static class HttpClientHelper
    {
        public static string BaseUrl = "http://localhost.fiddler:62375/";

        public static ApiResponse GET(string url)
        {
            var response = GetClient().GetAsync(url).Result;

            return GetApiResponse(response);
        }

        public static ApiResponse POST<T>(string url, T value)
        {
            var response = GetClient().PostAsJsonAsync(url, value).Result;

            return GetApiResponse(response);
        }

        private static HttpClient GetClient()
        {
            var client = new HttpClient
            {
                BaseAddress = new Uri(BaseUrl)
            };

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            return client;
        }

        private static ApiResponse GetApiResponse(HttpResponseMessage response)
        {
            var apiResponse = new ApiResponse
            {
                StatusCode = response.StatusCode,
                IsSuccessStatusCode = response.IsSuccessStatusCode
            };

            var formattedResponse = response.Content.ReadAsStringAsync().Result;
            apiResponse.Object = JsonConvert.DeserializeObject<dynamic>(formattedResponse);

            return apiResponse;
        }
    }

    public class ApiResponse
    {
        public dynamic Object { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public bool IsSuccessStatusCode { get; set; }
    }
}