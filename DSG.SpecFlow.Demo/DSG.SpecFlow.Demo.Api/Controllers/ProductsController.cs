using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace DSG.SpecFlow.Demo.Api.Controllers
{
    public class ProductsController : Controller
    {
        [HttpGet]
        [Route("api/product/{id}")]
        public object Get(int id)
        {
            if (id <= 0)
                return NotFound();

            return GetFakeProduct(id);
        }

        [HttpPost]
        [Route("api/product")]
        public object Post([FromBody]Product product)
        {
            if (string.IsNullOrWhiteSpace(product.Name))
            {
                return BadRequest(new
                {
                    ValidationMessages = new List<string>
                    {
                        "product name is required"
                    }
                });
            }

            var productId = new Random().Next(1, 100000);

            return Created($"api/product/{productId}", productId);
        }

        private Product GetFakeProduct(int productId)
        {
            return new Product
            {
                Id = productId,
                Name = $"product name {productId}",
                Description = $"product description {productId}"
            };
        }
    }
}