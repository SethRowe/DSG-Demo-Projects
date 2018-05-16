using System.Reflection;
using Autofac;
using DSG.NateApi.Demo.BLL.Managers;
using DSG.NateApi.Demo.DAL.Dapper;

namespace DSG.NateApi.Demo.BLL.Injection
{
    public class AutofacModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var bllAssembly = Assembly.GetAssembly(typeof(CustomerManager));
            var dalAssembly = Assembly.GetAssembly(typeof(DapperRepository));

            builder.RegisterAssemblyTypes(bllAssembly, dalAssembly).AsImplementedInterfaces();
        }
    }
}