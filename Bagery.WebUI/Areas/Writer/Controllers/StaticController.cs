using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bagery.WebUI.Areas.Writer.Controllers
{
    [Area("Writer")]
    [Authorize(Roles = "Writer")] // Writer paneli: sadece yazarlar
    public class StaticController : Controller
    {
        // Login'deki yönlendirme: RedirectToAction("Dashboard", "Static", new { area = "Writer" })
        public IActionResult Dashboard()
        {
            return View(); // Adım 5'te pixel-art istatistik ekranıyla değişecek
        }
    }
}