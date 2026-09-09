using Bagery.WebUI.MediatorPattern.Commands.CommentCommands;
using Bagery.WebUI.MediatorPattern.Queries.CommentQueries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bagery.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class CommentController(IMediator _mediator) : Controller
    {
        // Tüm yorumlar - sadece silme
        public async Task<IActionResult> Index()
        {
            var comments = await _mediator.Send(new GetCommentsQuery());
            return View(comments);
        }

        // Kendi yorumları - düzenleme + silme
        public async Task<IActionResult> MyComments()
        {
            var comments = await _mediator.Send(new GetCommentByUserIdQuery());
            return View(comments);
        }

        public async Task<IActionResult> UpdateComment(Guid Id)
        {
            var comment = await _mediator.Send(new GetCommentByIdQuery(Id));
            return View(comment);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateComment(UpdateCommentCommand updateCommentCommand)
        {
            await _mediator.Send(updateCommentCommand);
            return RedirectToAction(nameof(MyComments));
        }

        public async Task<IActionResult> DeleteComment(Guid Id, string? returnTo)
        {
            await _mediator.Send(new RemoveCommentCommand(Id));

            if (returnTo == "Index")
                return RedirectToAction(nameof(Index));

            return RedirectToAction(nameof(MyComments));
        }
    }
}