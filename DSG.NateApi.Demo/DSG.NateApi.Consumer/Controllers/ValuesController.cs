using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using DSG.NateApi.Consumer.BLL.Interfaces;

namespace DSG.NateApi.Consumer.Controllers
{
    public class ValuesController : ApiController
    {
        private readonly IConsumerManager _consumerManager;

        public ValuesController(IConsumerManager consumerManager)
        {
            _consumerManager = consumerManager;
        }

        [Route("api/values")]
        public async Task<IEnumerable<string>> Get()
        {
            await _consumerManager.DoTheWork();

            return new string[] { "value1", "value2" };
        }
    }
}
