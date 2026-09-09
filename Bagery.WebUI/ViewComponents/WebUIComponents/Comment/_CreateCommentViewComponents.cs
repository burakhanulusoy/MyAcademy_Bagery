// ViewComponents/CommentViewComponents/_CreateCommentViewComponent.cs
using Microsoft.AspNetCore.Mvc;

namespace Bagery.WebUI.ViewComponents.CommentViewComponents
{
    public class _CreateCommentViewComponents : ViewComponent
    {
        public IViewComponentResult Invoke(Guid blogId)
        {
            return View((object)blogId);
        }
    }
}