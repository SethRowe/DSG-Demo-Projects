using DSG.NateApi.Demo.BLL.Interfaces;

namespace DSG.NateApi.Demo.BLL.Managers
{
    public class EmailSubscriptionManager : IEmailSubscriptionManager
    {
        private readonly IMQUtility _mqUtility;

        public EmailSubscriptionManager(IMQUtility mqUtility)
        {
            _mqUtility = mqUtility;
        }

        public void DoSomeWork()
        {
            throw new System.NotImplementedException();
        }
    }
}