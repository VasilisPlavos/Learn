using System.Threading.Tasks;
using Example.Cloudon.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace Example.Cloudon.Web.Controllers
{
    public class ProductsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Edit(int? id)
        {
            return View(new ProductDto());
        }
    }
}
