using Bagery.WebUI.Enums;
using Bagery.WebUI.MediatorPattern.Queries.OrderQueries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bagery.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")] // müşteri bilgileri var: sadece admin
    public class OrderController(IMediator _mediator) : Controller
    {
        // /Admin/Order/Index  veya  /Admin/Order/Index?status=Paid
        public async Task<IActionResult> Index(OrderStatus? status)
        {
            var result = await _mediator.Send(new GetAdminOrdersQuery(status));
            return View(result);
        }

        // /Admin/Order/Detail?orderNo=...
        public async Task<IActionResult> Detail(string orderNo)
        {
            var result = await _mediator.Send(new GetAdminOrderDetailQuery(orderNo));
            if (result is null)
                return NotFound(); // Program.cs'teki ayar sayesinde senin 404 sayfan açılır

            return View(result);
        }
    }
}