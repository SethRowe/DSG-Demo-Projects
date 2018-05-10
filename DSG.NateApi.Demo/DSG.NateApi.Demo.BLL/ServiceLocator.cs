using System.Reflection;
using DSG.NateApi.Demo.BLL.Managers;
using DSG.NateApi.Demo.DAL.Dapper;
using Microsoft.Practices.Unity;

namespace DSG.NateApi.Demo.BLL
{
    /*
        =============
          READ THIS
        =============

        This class is for use by OUTSIDE callers and LEGACY business logic components ONLY.
                                 *******             ******                           ****

        New classes should follow the Dependency Injection pattern and NOT USE THIS CLASS
                                                                       ******************
    */

    public static class ServiceLocator
    {
        public static T Get<T>()
        {
            return UnityContainer.Resolve<T>();
        }

        private static UnityContainer _unityContainer;
        private static UnityContainer UnityContainer
        {
            get
            {
                if (_unityContainer == null)
                    BuildUnityContainer();

                return _unityContainer;
            }
        }

        private static void BuildUnityContainer()
        {
            _unityContainer = new UnityContainer();

            var bllAssembly = Assembly.GetAssembly(typeof(CustomerManager));
            var dalAssembly = Assembly.GetAssembly(typeof(DapperRepository));

            _unityContainer.RegisterTypes(
                AllClasses.FromAssemblies(bllAssembly, dalAssembly),
                WithMappings.FromAllInterfaces,
                WithName.Default,
                WithLifetime.Transient
            );
        }
    }
}