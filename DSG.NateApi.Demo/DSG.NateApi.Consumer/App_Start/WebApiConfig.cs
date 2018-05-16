using System.Reflection;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using DSG.NateApi.Consumer.BLL.Managers;
using DSG.NateApi.Consumer.DAL.Repositories;
using CRMAPI = DSG.NateApi.Demo.BLL;

namespace DSG.NateApi.Consumer
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            BuildAutofacContainer();

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }

        private static void BuildAutofacContainer()
        {
            var builder = new ContainerBuilder();

            var bllAssembly = Assembly.GetAssembly(typeof(ConsumerManager));
            var dalAssembly = Assembly.GetAssembly(typeof(ConsumerRepository));

            // Register our Web API controllers.
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            builder
                .RegisterAssemblyTypes(bllAssembly, dalAssembly)
                .AsImplementedInterfaces();

            // Register the components within the NateApi (previously hidden by ServiceLocator)
            builder.RegisterModule(new CRMAPI.Injection.AutofacModule());

            var container = builder.Build();

            // Set WebApi's DependencyResolver
            GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
    }
}
