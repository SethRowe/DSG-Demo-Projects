using System.Collections.Generic;
using System.Web.Http;

namespace DSG.Sample.NetFourSix.Api.Controllers
{
    public interface IValuesController
    {
        IEnumerable<string> Get();
        string Get(int id);
        void Post([FromBody]string value);
        void Put(int id, [FromBody]string value);
        void Delete(int id);
    }
}