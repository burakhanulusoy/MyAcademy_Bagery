using Bagery.WebUI.Enums;
using Bagery.WebUI.MediatorPattern.Commands.CartCommands;
using Bagery.WebUI.MediatorPattern.Commands.PaymentCommands;
using Bagery.WebUI.MediatorPattern.Queries.CheckoutQueries;
using Bagery.WebUI.MediatorPattern.Queries.InstallmentQueries;
using Bagery.WebUI.MediatorPattern.Queries.OrderQueries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bagery.WebUI.Controllers
{
    [Authorize]
    public class PaymentController(IMediator _mediator) : Controller
    {
        public async Task<IActionResult> Checkout()
        {
            var model = await _mediator.Send(new GetCheckoutQuery());

            if (model.IsCartEmpty)
                return RedirectToAction("Index", "Cart");

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Installments(GetInstallmentOptionsQuery query)
        {
            var installments = await _mediator.Send(query);
            return Json(new { success = true, installments });
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreatePaymentCommand command)
        {
            var result = await _mediator.Send(command);
            return Json(new { success = true, result.OrderNo, result.RedirectHtml });
        }

        // YENİ: merchant_ok_url -> PayTR başarılı 3D'den sonra kullanıcıyı buraya yollar
        public async Task<IActionResult> Success(string orderNo)
        {
            var order = await _mediator.Send(new GetMyOrderByOrderNoQuery(orderNo));

            // Sepet sadece burada silinir (callback'te session yok, Adım 9).
            // Callback "failed" demişse sepet kalsın, kullanıcı tekrar deneyebilsin.
            if (order is not null && order.Status != OrderStatus.Failed)
                await _mediator.Send(new ClearCartCommand());

            return View(order);
        }

        // YENİ: merchant_fail_url -> sepet SİLİNMEZ
        public async Task<IActionResult> Failure(string orderNo, string? fail_message)
        {
            var order = await _mediator.Send(new GetMyOrderByOrderNoQuery(orderNo));

            // Callback önce geldiyse gerçek sebep veritabanında yazılı; yoksa PAY_TR'deki gibi adresteki mesaj
            ViewBag.FailMessage = order?.Status == OrderStatus.Failed ? order.StatusMessage : fail_message;

            return View(order);
        }
    }
}