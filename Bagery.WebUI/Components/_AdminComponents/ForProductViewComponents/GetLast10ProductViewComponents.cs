using Bagery.WebUI.MediatorPattern.Queries.ProductQueries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Bagery.WebUI.Components._AdminComponents.ForProductViewComponents
{
    public class GetLast10ProductViewComponents(IMediator mediator):ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {

            var items = await mediator.Send(new GetProductLast10Query());
            return View(items);
        }

    }
}
