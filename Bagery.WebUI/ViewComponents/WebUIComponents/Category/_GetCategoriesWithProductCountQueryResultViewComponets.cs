using Bagery.WebUI.MediatorPattern.Queries.CategoryQueries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Bagery.WebUI.ViewComponents.WebUIComponents.Category
{
    public class _GetCategoriesWithProductCountQueryResultViewComponets(IMediator _mediator):ViewComponent
    {

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var items = await _mediator.Send(new GetCategoriesWithProductsQuery());
            return View(items);
        }


    }
}
