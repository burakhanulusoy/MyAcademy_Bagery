using Bagery.WebUI.MediatorPattern.Queries.OurHistoryQueries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Bagery.WebUI.ViewComponents.WebUIComponents.OurHistory
{
    public class GetOurHistoriesQueryResultViewComponents(IMediator _mediator):ViewComponent
    {

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var results = await _mediator.Send(new GetOurHistoryLastQuery());
             return View(results);

        }




    }
}
