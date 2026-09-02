using Bagery.WebUI.MediatorPattern.Queries.ProductQueries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Bagery.WebUI.ViewComponents.WebUIComponents.Product
{
    public class _GetProductsQueryResultViewComponents(IMediator _mediator):ViewComponent
    {

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var results = await _mediator.Send(new GetProductsWithRequiredValuesQuery());
            return View(results);
        }


    }
}
