using Bagery.WebUI.MediatorPattern.Queries.PanelQueries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bagery.WebUI.Areas.User.Controllers
{
    [Area("User")]
    [Authorize(Roles = "User")]
    public class StaticController(IMediator _mediator) : Controller
    {
        // /User/Static/Index (login sonrası yönlendirme)
        public async Task<IActionResult> Index()
        {
            var stats = await _mediator.Send(new GetUserDashboardQuery());
            return View(stats);
        }
    }
}