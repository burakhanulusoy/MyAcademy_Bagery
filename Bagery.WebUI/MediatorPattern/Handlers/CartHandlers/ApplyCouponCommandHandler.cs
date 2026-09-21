using Bagery.WebUI.Exceptions;
using Bagery.WebUI.MediatorPattern.Commands.CartCommands;
using Bagery.WebUI.Models.CartModels;
using Bagery.WebUI.Repositories.CouponRepositories;
using Bagery.WebUI.Services.CartServices;
using MediatR;

namespace Bagery.WebUI.MediatorPattern.Handlers.CartHandlers
{
    public class ApplyCouponCommandHandler(ICouponRepository _couponRepository,
                                           ICartService _cartService) : IRequestHandler<ApplyCouponCommand, Cart>
    {
        public async Task<Cart> Handle(ApplyCouponCommand request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(request.CouponCode))
                throw new BusinessException("Kupon kodunu girin.");

            var cart = _cartService.GetCart();
            if (cart.IsEmpty)
                throw new BusinessException("Kupon uygulamak için sepetinizde ürün olmalı.");

            // Adım 2'deki metot: büyük harfe çevirip arıyor
            var coupon = await _couponRepository.GetActiveByCodeAsync(request.CouponCode)
                         ?? throw new BusinessException("Kupon bulunamadı veya aktif değil.");

            // PAY_TR'deki MinPrice kuralı; :N2 = sayıyı 1.234,50 biçiminde yazar
            if (cart.SubTotal < coupon.MinPrice)
                throw new BusinessException($"Bu kupon {coupon.MinPrice:N2} ₺ ve üzeri sepetlerde geçerli.");

            cart.ApplyCoupon(coupon.CouponCode, coupon.CouponPrice, coupon.MinPrice);
            _cartService.SaveCart(cart);
            return cart;
        }
    }
}