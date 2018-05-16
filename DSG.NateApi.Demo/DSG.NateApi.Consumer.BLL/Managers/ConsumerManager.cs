using System.Threading.Tasks;
using DSG.NateApi.Consumer.BLL.Interfaces;
using DSG.NateApi.Consumer.DAL;

namespace DSG.NateApi.Consumer.BLL.Managers
{
    public class ConsumerManager : IConsumerManager
    {
        private readonly IConsumerRepository _consumerRepository;

        public ConsumerManager(IConsumerRepository consumerRepository)
        {
            _consumerRepository = consumerRepository;
        }

        public async Task DoTheWork()
        {
            await _consumerRepository.DoTheWork();
        }
    }
}