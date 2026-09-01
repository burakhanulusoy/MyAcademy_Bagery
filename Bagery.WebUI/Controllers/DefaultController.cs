using Microsoft.AspNetCore.Mvc;

namespace Bagery.WebUI.Controllers
{
    public class DefaultController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
