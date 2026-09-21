using Bagery.WebUI.MediatorPattern.Commands.CouponCommands;
using Bagery.WebUI.MediatorPattern.Queries.CouponQueries;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bagery.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")] // indirim = para; sadece admin
    public class CouponController(IMediator _mediator) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var coupons = await _mediator.Send(new GetCouponsQuery());
            return View(coupons);
        }

        public IActionResult CreateCoupon()
        {
            return View(new CreateCouponCommand()); // IsActive varsayılan true gelsin diye boş model
        }

        [HttpPost]
        public async Task<IActionResult> CreateCoupon(CreateCouponCommand command)
        {
            await _mediator.Send(command); // hata olursa ExceptionFilter formu mesajlarla geri açar
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> UpdateCoupon(Guid id)
        {
            var coupon = await _mediator.Send(new GetCouponByIdQuery(id));
            if (coupon is null)
                return NotFound();

            return View(coupon.Adapt<UpdateCouponCommand>()); // PromotionController'daki yöntem
        }

        [HttpPost]
        public async Task<IActionResult> UpdateCoupon(UpdateCouponCommand command)
        {
            await _mediator.Send(command);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> ToggleCoupon(Guid id)
        {
            await _mediator.Send(new ToggleCouponStatusCommand(id));
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> DeleteCoupon(Guid id)
        {
            await _mediator.Send(new DeleteCouponCommand(id));
            return RedirectToAction(nameof(Index));
        }
    }
}