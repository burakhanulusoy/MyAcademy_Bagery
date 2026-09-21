using Bagery.WebUI.MediatorPattern.Commands.PaymentCommands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bagery.WebUI.Controllers
{
    // PayTR panelindeki "Bildirim URL": https://<ngrok-adresin>/CallBack/Index
    [AllowAnonymous]          // PayTR'nin sunucusu giriş yapmış bir kullanıcı değil
    [IgnoreAntiforgeryToken]  // PayTR antiforgery token gönderemez (ileride global doğrulama açılırsa diye)
    public class CallBackController(IMediator _mediator) : Controller
    {
        [HttpPost]
        public async Task<IActionResult> Index()
        {
            // PayTR alan adları alt çizgili (merchant_oid); PAY_TR'deki gibi formdan tek tek okuyoruz
            var form = await Request.ReadFormAsync();

            var response = await _mediator.Send(new ProcessPaymentCallbackCommand(
                MerchantOid: form["merchant_oid"].ToString(),
                Status: form["status"].ToString(),
                TotalAmount: form["total_amount"].ToString(),
                Hash: form["hash"].ToString(),
                FailedReasonCode: form["failed_reason_code"].ToString(),
                FailedReasonMsg: form["failed_reason_msg"].ToString()));

            // Düz metin: PayTR sadece "OK" kelimesini arıyor, HTML ya da JSON değil
            return Content(response, "text/plain");
        }
    }
}