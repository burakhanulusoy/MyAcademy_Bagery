using Bagery.WebUI.MediatorPattern.Queries.OrderQueries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bagery.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize] // giriş yapan herkes girebilir; sorgular sadece kendi siparişlerini getirir
    public class MyOrdersController(IMediator _mediator) : Controller
    {
        // /Admin/MyOrders/Index
        public async Task<IActionResult> Index()
        {
            var orders = await _mediator.Send(new GetMyOrdersQuery());
            return View(orders);
        }

        // /Admin/MyOrders/Detail?orderNo=...
        public async Task<IActionResult> Detail(string orderNo)
        {
            var order = await _mediator.Send(new GetMyOrderByOrderNoQuery(orderNo));
            if (order is null)
                return NotFound();

            return View(order);
        }

        // /Admin/MyOrders/Invoice?orderNo=...                 -> tarayıcıda açılır
        // /Admin/MyOrders/Invoice?orderNo=...&download=true   -> dosya olarak iner
        public async Task<IActionResult> Invoice(string orderNo, bool download = false)
        {
            var invoice = await _mediator.Send(new GetMyOrderInvoiceQuery(orderNo));
            if (invoice is null)
                return NotFound();

            if (download)
                return File(invoice.Content, "application/pdf", invoice.FileName); // "attachment": indir

            // "inline": tarayıcının PDF görüntüleyicisinde açılır (oradan da indirilebilir)
            Response.Headers.ContentDisposition = $"inline; filename=\"{invoice.FileName}\"";
            return File(invoice.Content, "application/pdf");
        }
    }
}