using System.Net;
using DSG.NateApi.Demo.BLL;
using DSG.NateApi.Demo.BLL.Interfaces;
using DSG.NateApi.Demo.BLL.Legacy.Interfaces;
using DSG.NateApi.Demo.BLL.Legacy.Wrappers;
using DSG.NateApi.Demo.BLL.Managers;
using DSG.NateApi.Demo.BLL.Utilities;
using DSG.NateApi.Demo.DAL.Dapper;
using DSG.NateApi.Demo.DAL.Interfaces;
using DSG.NateApi.Demo.DAL.Legacy.Interfaces;
using DSG.NateApi.Demo.DAL.Legacy.Wrappers;
using Microsoft.Practices.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DSG.NateApi.Demo.Tests
{
    [TestClass]
    public class ServiceLocatorTests
    {
        [TestMethod]
        public void ShouldBeAbleToResolveManager()
        {
            var customerManager = ServiceLocator.Get<ICustomerManager>();

            Assert.IsNotNull(customerManager);
            Assert.IsInstanceOfType(customerManager, typeof(CustomerManager));
        }

        [TestMethod]
        public void ShouldBeAbleToResolveLegacyBLL()
        {
            var legacyManager = ServiceLocator.Get<ILegacyManager>();

            Assert.IsNotNull(legacyManager);
            Assert.IsInstanceOfType(legacyManager, typeof(LegacyManagerWrapper));
        }

        [TestMethod]
        public void ShouldBeAbleToResolveUtilityClass()
        {
            var mqUtility = ServiceLocator.Get<IMQUtility>();

            Assert.IsNotNull(mqUtility);
            Assert.IsInstanceOfType(mqUtility, typeof(MQUtility));
        }

        [TestMethod]
        public void ShouldBeAbleToResolveDapperRepository()
        {
            var dapperRepository = ServiceLocator.Get<IDapperRepository>();

            Assert.IsNotNull(dapperRepository);
            Assert.IsInstanceOfType(dapperRepository, typeof(DapperRepository));
        }

        [TestMethod]
        public void ShouldBeAbleToResolveLegacyRepository()
        {
            var legacyRepository = ServiceLocator.Get<ILegacyRepository>();

            Assert.IsNotNull(legacyRepository);
            Assert.IsInstanceOfType(legacyRepository, typeof(LegacyRepositoryWrapper));
        }

        [TestMethod]
        public void ShouldNotBeAbleToResolveAForeignType()
        {
            // I picked ICertificatePolicy at random from Intellisense. It's not one of
            // our types in the BLL or DAL, so the ServiceLocator should fail to resolve it.

            Assert.ThrowsException<ResolutionFailedException>(() => ServiceLocator.Get<ICertificatePolicy>());
        }
    }
}
