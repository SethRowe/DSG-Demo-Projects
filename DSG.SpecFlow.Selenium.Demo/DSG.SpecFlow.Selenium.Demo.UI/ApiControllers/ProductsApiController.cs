using Microsoft.AspNetCore.Mvc;

namespace DSG.SpecFlow.Selenium.Demo.UI.ApiControllers
{
    public class ProductsApiController : Controller
    {
        [Route("api/dropdowns/{dropdownId}")]
        public IActionResult Get(int dropdownId)
        {
            if (dropdownId == 4) // Load subcategories
            {
                return Ok(new[]
                {
                    new {id = 21, text = "Empty sub-category one"},
                    new {id = 22, text = "Empty sub-category two"},
                    new {id = 23, text = "Empty sub-category three"},
                    new {id = 24, text = "Sub-category with products and sub-products"}
                });
            }

            if (dropdownId == 24) // Load products & sub-products
            {
                return Ok(new
                {
                    products = new[]
                    {
                        new {id = 31, text = "Product one"},
                        new {id = 32, text = "Product two"},
                        new {id = 33, text = "Product three"},
                    },
                    subProducts = new[]
                    {
                        new {id = 41, text = "Sub-product one"},
                        new {id = 42, text = "Sub-product two"},
                        new {id = 43, text = "Sub-product three"},
                    }
                });
            }

            return Ok();
        }
    }
}