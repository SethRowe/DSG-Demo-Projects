using System.Reflection;
using System.Web.Http;
using DSG.UnityDI.Common;
using DSG.UnityDI.Managers;
using DSG.UnityDI.Repositories;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.WebApi;

namespace DSG.UnityDI.Demo
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            BuildUnityContainer();
            config.MapHttpAttributeRoutes();
        }

        private static void BuildUnityContainer()
        {
            // Create the Unity container
            var container = new UnityContainer();

            // Register our types by convention (i.e. typescanner)
            container.RegisterTypes(
                AllClasses.FromAssemblies(
                    Assembly.GetAssembly(typeof(PromoManager)),
                    Assembly.GetAssembly(typeof(PromoRepository))
                ),
                WithMappings.FromAllInterfaces,
                WithName.Default,
                WithLifetime.Transient
            );

            // Manually register IUnitOfWork, so we can override its lifetime (see comments below)
            container.RegisterType<IUnitOfWork, UnitOfWork>(new PerResolveLifetimeManager());

            // Set WebApi's DependencyResolver
            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }

        /*
         * Please see section 'Lifetime Management' in this article for reference:
         *   https://msdn.microsoft.com/en-us/library/dn178463(v=pandp.30).aspx
         *
         * Using PerResolveLifetimeManager means that when resolving the ApiController,
         * any reference it or its children has to IUnitOfWork will resolve the same
         * UnitOfWork item. Since the ApiControllers are registered as Transient, this means
         * each request will gets its own ApiController, with a common IUnitOfWork throughout
         * its dependency chain.
         *
         * PerThreadLifetimeManager is close, but will start to reuse UnitOfWork classes
         * as the ThreadPool starts to reuse threads. It's very misleading when testing...
         * 
         * Note: One side effect is that you can't use PerResolveLifetimeManager along with the
         * Lazy<> for lazy loading dependencies. When the Lazy<> object gets its value, it
         * counts as a seperate Resolve and will get a different UnitOfWork from Unity.
         *
         */
    }
}