using Bagery.WebUI.MediatorPattern.Queries.PanelQueries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bagery.WebUI.Areas.Writer.Controllers
{
    [Area("Writer")]
    [Authorize(Roles = "Writer")]
    public class StaticController(IMediator _mediator) : Controller
    {
        // /Writer/Static/Dashboard (login sonrası yönlendirme)
        public async Task<IActionResult> Dashboard()
        {
            var stats = await _mediator.Send(new GetWriterDashboardQuery());
            return View(stats);
        }
    }
}