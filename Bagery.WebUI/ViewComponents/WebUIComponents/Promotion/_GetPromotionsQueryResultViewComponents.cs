using Bagery.WebUI.MediatorPattern.Queries.PromotionQueries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Bagery.WebUI.ViewComponents.WebUIComponents.Promotion
{
    public class _GetPromotionsQueryResultViewComponents(IMediator mediator):ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var results = await mediator.Send(new GetPromotionsQuery());
            return View(results);
        }


    }
}
