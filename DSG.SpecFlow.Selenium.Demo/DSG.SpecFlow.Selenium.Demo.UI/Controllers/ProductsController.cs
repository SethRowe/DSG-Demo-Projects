using Microsoft.AspNetCore.Mvc;

namespace DSG.SpecFlow.Selenium.Demo.UI.Controllers
{
    public class ProductsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}