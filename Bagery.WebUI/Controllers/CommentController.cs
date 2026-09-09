using Bagery.WebUI.Exceptions;
using Bagery.WebUI.MediatorPattern.Commands.CommentCommands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bagery.WebUI.Controllers
{
    public class CommentController(IMediator _mediator) : Controller
    {
        [HttpPost]
        [Authorize(Roles = "Admin,Writer")]
        public async Task<IActionResult> CreateComment(
            CreateCommentCommand command)
        {
            try
            {
                await _mediator.Send(command);
                TempData["CommentSuccess"] = "Yorumunuz yayınlandı.";
            }
            catch (ValidationUIException ex)
            {
                TempData["CommentError"] =
                    string.Join(" ", ex.Errors.Select(x => x.ErrorMessage));
            }
            catch (ContentModerationException ex)
            {
                TempData["CommentError"] = ex.Message;
            }
            catch (IdentityException ex)
            {
                TempData["CommentError"] = ex.Message;
            }

            return RedirectToAction(
                "Detail",
                "Blog",
                new { Id = command.BlogId });
        }
    }
}