using System.Threading.Tasks;
using CRMAPI = DSG.NateApi.Demo.BLL.Interfaces;

namespace DSG.NateApi.Consumer.DAL.Repositories
{
    public class ConsumerRepository : IConsumerRepository
    {
        private readonly CRMAPI.ILegacyManager _legacyManager;
        private readonly CRMAPI.ICustomerManager _customerManager;
        
        public ConsumerRepository(CRMAPI.ILegacyManager legacyManager, CRMAPI.ICustomerManager customerManager)
        {
            _legacyManager = legacyManager;
            _customerManager = customerManager;
        }

        public async Task DoTheWork()
        {
            await Task.Run(() =>
            {
                _legacyManager.DoTheWork();
                _customerManager.DoSomeWork();
            });
        }
    }
}