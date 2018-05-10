using System.Net.Http.Headers;
using System.Web;
using System.Web.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace DSG.UnityDI.Demo
{
    public class WebApiApplication : HttpApplication
    {
        protected void Application_Start()
        {
            json_please();

            GlobalConfiguration.Configure(WebApiConfig.Register);
        }

        private void json_please()
        {
            var formatter = GlobalConfiguration.Configuration.Formatters.JsonFormatter;
            formatter.SerializerSettings = new JsonSerializerSettings
            {
                Formatting = Formatting.Indented,
                TypeNameHandling = TypeNameHandling.Objects,
                ContractResolver = new CamelCasePropertyNamesContractResolver(),

            };

            formatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));
        }
    }
}