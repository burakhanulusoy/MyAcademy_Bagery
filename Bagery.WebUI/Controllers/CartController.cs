using Bagery.WebUI.MediatorPattern.Commands.CartCommands;
using Bagery.WebUI.MediatorPattern.Queries.CartQueries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Bagery.WebUI.Controllers
{
    // Index hariç hepsi fetch ile çağrılır ve JSON döner.
    // Hata olursa handler exception fırlatır, ExceptionFilter { success = false, message } döner.
    // Bu yüzden burada try-catch yok.
    public class CartController(IMediator _mediator) : Controller
    {
        // Sepet sayfası (view'ı Adım 5'te yazacağız)
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetCart()
        {
            var cart = await _mediator.Send(new GetCartQuery());
            return Json(new { success = true, cart });
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddToCartCommand command)
        {
            var cart = await _mediator.Send(command);
            return Json(new { success = true, message = "Ürün sepete eklendi.", cart });
        }

        [HttpPost]
        public async Task<IActionResult> Decrease(DecreaseCartItemCommand command)
        {
            var cart = await _mediator.Send(command);
            return Json(new { success = true, cart });
        }

        [HttpPost]
        public async Task<IActionResult> Remove(RemoveCartItemCommand command)
        {
            var cart = await _mediator.Send(command);
            return Json(new { success = true, message = "Ürün sepetten çıkarıldı.", cart });
        }

        [HttpPost]
        public async Task<IActionResult> ApplyCoupon(ApplyCouponCommand command)
        {
            var cart = await _mediator.Send(command);
            return Json(new { success = true, message = "Kupon uygulandı.", cart });
        }

        [HttpPost]
        public async Task<IActionResult> RemoveCoupon()
        {
            var cart = await _mediator.Send(new RemoveCouponCommand());
            return Json(new { success = true, message = "Kupon kaldırıldı.", cart });
        }
    }
}