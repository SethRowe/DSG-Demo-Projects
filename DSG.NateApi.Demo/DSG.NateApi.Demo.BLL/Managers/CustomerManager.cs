using DSG.NateApi.Demo.BLL.Interfaces;
using DSG.NateApi.Demo.DAL.Interfaces;

namespace DSG.NateApi.Demo.BLL.Managers
{
    public class CustomerManager : ICustomerManager
    {
        private readonly ILegacyManager _legacyManager;
        private readonly IEmailSubscriptionManager _emailSubscriptionManager;
        private readonly IDapperRepository _dapperRepository;

        public CustomerManager(ILegacyManager legacyManager, IEmailSubscriptionManager emailSubscriptionManager, IDapperRepository dapperRepository)
        {
            _legacyManager = legacyManager;
            _emailSubscriptionManager = emailSubscriptionManager;
            _dapperRepository = dapperRepository;
        }

        public void DoSomeWork()
        {
            // Do some work
        }
    }
}