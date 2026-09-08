using Bagery.WebUI.MediatorPattern.Queries.BlogQueries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Bagery.WebUI.ViewComponents.WebUIComponents.Blog
{
    public class _GetLast3BlogQueryResultViewComponents(IMediator _mediator):ViewComponent
    {

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var items = await _mediator.Send(new GetBlogLast4Query());
            return View(items);
        }


    }
}
