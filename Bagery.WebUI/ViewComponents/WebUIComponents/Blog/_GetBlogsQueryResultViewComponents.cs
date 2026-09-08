using Bagery.WebUI.MediatorPattern.Queries.BlogQueries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Bagery.WebUI.ViewComponents.WebUIComponents.Blog
{
    public class _GetBlogsQueryResultViewComponents(IMediator mediator):ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var items = await mediator.Send(new GetBlogsWithUserQuery());
            return View(items);
        }


    }
}
