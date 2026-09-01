using Bagery.WebUI.MediatorPattern.Queries.CategoryQueries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Bagery.WebUI.ViewComponents.WebUIComponents.SpecialMenu
{
    public class _GetCategoryWithLastProductViewComponents(IMediator _mediator):ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var values = await _mediator.Send(new GetCategeoryWithLastProductForSpecialMenuQuery());
            return View(values);
        }

    }
}
