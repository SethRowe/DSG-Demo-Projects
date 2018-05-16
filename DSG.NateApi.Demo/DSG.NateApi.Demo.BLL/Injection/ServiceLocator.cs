using System.Reflection;
using Autofac;
using DSG.NateApi.Demo.BLL.Injection;
using DSG.NateApi.Demo.BLL.Managers;
using DSG.NateApi.Demo.DAL.Dapper;

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
      
        =============
          ALSO NOTE
        =============

        For outside consumers using Autofac, they can ignore this class and instead use the 
        Injection.AutofacModule (in conjuction with builder.RegisterModule(...)) to consume
        our libraries from within their Autofac container.

    */

    public static class ServiceLocator
    {
        public static T Get<T>()
        {
            return Container.Resolve<T>();
        }

        private static IContainer _container;
        private static IContainer Container
        {
            get
            {
                if (_container == null)
                    BuildUnityContainer();

                return _container;
            }
        }

        private static void BuildUnityContainer()
        {
            var builder = new ContainerBuilder();

            builder.RegisterModule(new AutofacModule());

            _container = builder.Build();
        }
    }
}