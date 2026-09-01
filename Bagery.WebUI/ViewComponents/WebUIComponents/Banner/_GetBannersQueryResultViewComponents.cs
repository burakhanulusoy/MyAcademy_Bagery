using Bagery.WebUI.MediatorPattern.Queries.BannerQueries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Bagery.WebUI.ViewComponents.WebUIComponents.Banner
{
    public class _GetBannersQueryResultViewComponents(IMediator _mediator):ViewComponent
    {

        public async Task<IViewComponentResult> InvokeAsync()
        {

            var result = await _mediator.Send(new GetBannersQuery());
            return View(result);

        }



    }
}
