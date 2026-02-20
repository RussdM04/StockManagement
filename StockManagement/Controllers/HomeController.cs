using Microsoft.AspNetCore.Mvc;

namespace StockManagement.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
