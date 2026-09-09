using Bagery.WebUI.MediatorPattern.Queries.CommentQueries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Bagery.WebUI.ViewComponents.WebUIComponents.Comment
{
    public class _GetCommentsByBlogIdViewComponents(IMediator _mediator):ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(Guid blogId)
        {
            var comments = await _mediator.Send(new GetCommentsByBlogIdQuery(blogId));
            return View(comments);
        }
    }
}
