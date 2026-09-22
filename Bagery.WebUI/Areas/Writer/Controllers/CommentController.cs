using Bagery.WebUI.Exceptions;
using Bagery.WebUI.MediatorPattern.Commands.CommentCommands;
using Bagery.WebUI.MediatorPattern.Queries.CommentQueries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bagery.WebUI.Areas.Writer.Controllers
{
    [Area("Writer")]
    [Authorize(Roles = "Writer")]
    public class CommentController(IMediator _mediator) : Controller
    {
        // Sadece listeleme: yorum oluşturma sitedeki blog sayfasında, güncelleme panelde yok
        public async Task<IActionResult> Index()
        {
            var comments = await _mediator.Send(new GetCommentByUserIdQuery());
            return View(comments);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteComment(Guid id)
        {
            try
            {
                await _mediator.Send(new RemoveCommentCommand(id)); // "sadece kendi yorumu" kontrolü handler'da
            }
            catch (IdentityException)
            {
                // Başkasının yorumu ya da olmayan yorum: hata sayfası yerine 404
                return NotFound();
            }

            TempData["PanelSuccess"] = "Yorumun silindi.";
            return RedirectToAction(nameof(Index));
        }
    }
}