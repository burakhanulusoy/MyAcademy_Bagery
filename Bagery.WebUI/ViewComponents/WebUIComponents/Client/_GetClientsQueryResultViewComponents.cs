using Bagery.WebUI.MediatorPattern.Queries.ClientQueries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Bagery.WebUI.ViewComponents.WebUIComponents.Client
{
    public class _GetClientsQueryResultViewComponents(IMediator mediator):ViewComponent
    {

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var result = await mediator.Send(new GetClientsQuery());
            return View(result);
        }


    }
}
