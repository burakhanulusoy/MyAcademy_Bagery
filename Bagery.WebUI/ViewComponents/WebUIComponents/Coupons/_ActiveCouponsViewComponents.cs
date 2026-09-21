using Bagery.WebUI.MediatorPattern.Queries.CouponQueries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Bagery.WebUI.ViewComponents.WebUIComponents.Coupons
{
    // Senin diğer ViewComponent'lerin gibi isim "ViewComponents" ile bitiyor;
    // view dosyası: Views/Shared/Components/_ActiveCouponsViewComponents/Default.cshtml
    public class _ActiveCouponsViewComponents(IMediator _mediator) : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var coupons = await _mediator.Send(new GetActiveCouponsQuery());
            return View(coupons);
        }
    }
}