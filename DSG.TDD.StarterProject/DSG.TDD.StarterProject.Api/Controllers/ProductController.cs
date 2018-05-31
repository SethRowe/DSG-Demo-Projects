using System;
using System.Threading.Tasks;
using DSG.TDD.StarterProject.Managers.Entities;
using DSG.TDD.StarterProject.Managers.Managers;
using Microsoft.AspNetCore.Mvc;

namespace DSG.TDD.StarterProject.Api.Controllers
{
    [Route("api/[controller]")]
    public class ProductController : Controller
    {
        private readonly IProductManager _productManager;

        public ProductController(IProductManager productManager)
        {
            _productManager = productManager;
        }

        // GET api/product/5
        [HttpGet("{id}")]
        public async Task<ActionResult> Get(long id)
        {
            try
            {
                if (id <= 0)
                    return BadRequest();

                var product = await _productManager.GetItem(id);

                if (product == null)
                    return NotFound();

                return Ok(product);
            }
            catch
            {
                return StatusCode(500);
            }
        }

        // POST api/product
        [HttpPost]
        public ActionResult Post([FromBody]Product value)
        {
            throw new NotImplementedException();
        }

        // DELETE api/product/5
        [HttpDelete("{id}")]
        public ActionResult Delete(long id)
        {
            throw new NotImplementedException();
        }
    }
}
