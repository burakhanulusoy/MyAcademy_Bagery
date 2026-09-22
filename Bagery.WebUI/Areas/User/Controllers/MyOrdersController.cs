using Bagery.WebUI.MediatorPattern.Queries.OrderQueries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bagery.WebUI.Areas.User.Controllers
{
    [Area("User")]
    [Authorize(Roles = "User")]
    public class MyOrdersController(IMediator _mediator) : Controller
    {
        // /User/MyOrders/Index
        public async Task<IActionResult> Index()
        {
            var orders = await _mediator.Send(new GetMyOrdersQuery()); // sadece giriş yapanın siparişleri
            return View(orders);
        }

        // /User/MyOrders/Detail?orderNo=...
        public async Task<IActionResult> Detail(string orderNo)
        {
            var order = await _mediator.Send(new GetMyOrderByOrderNoQuery(orderNo)); // başkasınınsa null
            if (order is null)
                return NotFound();

            return View(order);
        }

        // /User/MyOrders/Invoice?orderNo=...                -> tarayıcıda açılır
        // /User/MyOrders/Invoice?orderNo=...&download=true  -> dosya olarak iner
        public async Task<IActionResult> Invoice(string orderNo, bool download = false)
        {
            var invoice = await _mediator.Send(new GetMyOrderInvoiceQuery(orderNo)); // ödenmemişse / başkasınınsa null
            if (invoice is null)
                return NotFound();

            if (download)
                return File(invoice.Content, "application/pdf", invoice.FileName);

            Response.Headers.ContentDisposition = $"inline; filename=\"{invoice.FileName}\"";
            return File(invoice.Content, "application/pdf");
        }
    }
}