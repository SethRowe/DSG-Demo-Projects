using System.Collections.Generic;
using System.Web.Http;

namespace DSG.Sample.NetFourSix.Api.Controllers
{
    public class ValuesController : ApiController, IValuesController
    {
        // GET api/values
        [Route("api/values")]
        public IEnumerable<string> Get()
        {
            return new string[] { "net four six one", "net four six two" };
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
