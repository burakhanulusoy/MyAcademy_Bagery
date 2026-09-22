using Bagery.WebUI.MediatorPattern.Commands.BlogCommands;
using Bagery.WebUI.MediatorPattern.Queries.BlogQueries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Bagery.WebUI.Areas.Writer.Controllers
{
    [Area("Writer")]
    [Authorize(Roles = "Writer")]
    public class BlogController(IMediator _mediator) : Controller
    {
        // Sadece kendi blogları (sorgu giriş yapan kullanıcıya göre filtreliyor)
        public async Task<IActionResult> Index()
        {
            var blogs = await _mediator.Send(new GetBlogByUserIdQuery());
            return View(blogs);
        }

        public IActionResult CreateBlog()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateBlog(CreateBlogCommand command)
        {
            await _mediator.Send(command); // hata varsa ExceptionFilter formu mesajlarla geri açar
            TempData["PanelSuccess"] = "Blogun yayınlandı.";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> UpdateBlog(Guid id)
        {
            var blog = await _mediator.Send(new GetBlogByIdQuery(id));

            // Başkasının blogunu açmaya çalışırsa: yokmuş gibi davran
            if (blog is null || blog.AppUserId != CurrentUserId())
                return NotFound();

            return View(blog);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateBlog(UpdateBlogCommand command)
        {
            await _mediator.Send(command); // sahiplik kontrolü handler'da (Adım 0.3)
            TempData["PanelSuccess"] = "Blogun güncellendi.";
            return RedirectToAction(nameof(Index));
        }

        // Silme POST + token: bir link tıklatılarak ya da başka siteden tetiklenerek silinemez
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteBlog(Guid id)
        {
            await _mediator.Send(new RemoveBlogCommand(id)); // sahiplik kontrolü handler'da (Adım 0.2)
            TempData["PanelSuccess"] = "Blog silindi.";
            return RedirectToAction(nameof(Index));
        }

        // Giriş yapan kullanıcının Id'si (cookie'den, veritabanına gitmeden)
        private Guid CurrentUserId() => Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
    }
}