using Microsoft.AspNetCore.Mvc;

namespace Example.Cloudon.Web.Controllers
{
    public class ProductsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
