using Bagery.WebUI.MediatorPattern.Queries.CategoryQueries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Bagery.WebUI.ViewComponents.WebUIComponents.Category
{
    public class _GetCategoriesWithProductsQueryResultViewComponents(IMediator _mediator):ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var result = await _mediator.Send(new GetCategoriesWithProductsQuery());
            return View(result);
        }


    }
}
