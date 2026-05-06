using Microsoft.AspNetCore.Mvc;

namespace Bagery.WebUI.Areas.Admin.Controllers
{
    public class ProductVariantController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
