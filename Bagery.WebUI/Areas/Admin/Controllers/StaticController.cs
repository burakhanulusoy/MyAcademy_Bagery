using Bagery.WebUI.MediatorPattern.Queries.DashboardQueries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bagery.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")] // ciro ve müşteri verisi: sadece admin
    public class StaticController(IMediator _mediator) : Controller
    {
        // /Admin/Static/Dashboard?days=30  -> Satış Paneli
        public async Task<IActionResult> Dashboard(int days = 30)
        {
            return View(await _mediator.Send(new GetSalesDashboardQuery(days)));
        }

        // /Admin/Static/Overview -> Site Genel Bakış
        public async Task<IActionResult> Overview()
        {
            return View(await _mediator.Send(new GetSiteOverviewQuery()));
        }
    }
}