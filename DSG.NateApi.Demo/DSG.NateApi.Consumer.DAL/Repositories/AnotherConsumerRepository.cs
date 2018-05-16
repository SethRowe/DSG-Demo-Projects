using System.Threading.Tasks;
using CRMAPI = DSG.NateApi.Demo.BLL.Interfaces;

namespace DSG.NateApi.Consumer.DAL.Repositories
{
    public class AnotherConsumerRepository : IAnotherConsumerRepository
    {
        private readonly CRMAPI.IEmailSubscriptionManager _emailSubscriptionManager;

        public AnotherConsumerRepository(CRMAPI.IEmailSubscriptionManager emailSubscriptionManager)
        {
            _emailSubscriptionManager = emailSubscriptionManager;
        }

        public async Task DoTheWork()
        {
            await Task.Run(() =>
            {
                _emailSubscriptionManager.DoSomeWork();
            });
        }
    }
}