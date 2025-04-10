using Microsoft.AspNetCore.Mvc;

namespace duanagile.Controllers
{
    public class CustomerController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
