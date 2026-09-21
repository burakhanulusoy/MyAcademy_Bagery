using Bagery.WebUI.Entities;
using Bagery.WebUI.Enums;
using Bagery.WebUI.Exceptions;
using Bagery.WebUI.MediatorPattern.Commands.PaymentCommands;
using Bagery.WebUI.MediatorPattern.Results.PaymentResults;
using Bagery.WebUI.Models.CartModels;
using Bagery.WebUI.Repositories.CouponRepositories;
using Bagery.WebUI.Repositories.InstallmentRepositories;
using Bagery.WebUI.Repositories.OrderRepositories;
using Bagery.WebUI.Repositories.ProductRepositories;
using Bagery.WebUI.Services.CartServices;
using Bagery.WebUI.Services.PayTRServices;
using Bagery.WebUI.UOW;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Bagery.WebUI.MediatorPattern.Handlers.PaymentHandlers
{
    public class CreatePaymentCommandHandler(IValidator<CreatePaymentCommand> _validator,
                                             UserManager<AppUser> _userManager,
                                             IHttpContextAccessor _httpContextAccessor,
                                             ICartService _cartService,
                                             IProductRepository _productRepository,
                                             ICouponRepository _couponRepository,
                                             IInstallmentRepository _installmentRepository,
                                             IOrderRepository _orderRepository,
                                             IUnitOfWork _unitOfWork,
                                             IPayTRService _payTRService,
                                             IConfiguration _configuration) : IRequestHandler<CreatePaymentCommand, CreatePaymentResult>
    {
        public async Task<CreatePaymentResult> Handle(CreatePaymentCommand request, CancellationToken cancellationToken)
        {
            // 1) Form doğrulama
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
                throw new ValidationUIException(validationResult.Errors);

            // 2) Giriş yapan kullanıcı (PAY_TR'deki sabit UserModel'in yerine)
            var httpContext = _httpContextAccessor.HttpContext!;
            var user = await _userManager.GetUserAsync(httpContext.User)
                       ?? throw new IdentityException("Ödeme yapmak için giriş yapmalısınız.");

            // 3) Sepet
            var cart = _cartService.GetCart();
            if (cart.IsEmpty)
                throw new BusinessException("Sepetiniz boş.");

            await EnsureCartIsUpToDateAsync(cart); // açıklaması metodun üstünde

            if (cart.PaidPrice <= 0)
                throw new BusinessException("Ödenecek tutar 0 olamaz.");

            // 4) Taksit (PAY_TR'deki "if (installment > 0)" bloğu)
            var cardNumber = new string(request.CardNumber!.Where(char.IsDigit).ToArray()); // boşlukları at
            var paidPrice = cart.PaidPrice;
            var cardBrand = string.Empty; // tek çekimde boş gider (PAY_TR'deki gibi)

            if (request.InstallmentCount > 0)
            {
                var card = await _payTRService.GetCardInfoAsync(cardNumber[..8], cancellationToken);
                if (!card.IsCreditCard)
                    throw new BusinessException("Taksit yalnızca kredi kartlarında yapılabilir. Tek çekim seçin.");

                var installment = await _installmentRepository.GetAsync(card.Brand, request.InstallmentCount)
                                  ?? throw new BusinessException("Seçilen taksit bu kart için geçerli değil.");

                // Taksit tablosunda gösterilen tutarla aynı formül (Adım 7.1)
                paidPrice = InstallmentCalculator.ApplyRate(paidPrice, installment.Rate);
                cardBrand = card.Brand;
            }

            // 5) Sipariş nesnesi (henüz KAYDEDİLMİYOR, 7. maddeye bak)
            var orderNo = Guid.NewGuid().ToString("N"); // "N": tire olmadan, sadece harf+rakam (PayTR kuralı)

            var order = new Order
            {
                OrderNo = orderNo,
                Status = OrderStatus.Pending,               // sonucu callback belirleyecek (Adım 9)
                StatusMessage = "PayTR bildirimi bekleniyor.",
                TotalPrice = cart.SubTotal,
                ShippingPrice = cart.ShippingPrice,
                CouponCode = cart.CouponCode,
                CouponPrice = cart.CouponDiscount,
                PaidPrice = paidPrice,                      // taksit farkı dahil
                InstallmentCount = request.InstallmentCount,
                CardBrand = string.IsNullOrEmpty(cardBrand) ? null : cardBrand,
                FullName = request.FullName!.Trim(),        // ! = validator boş olmadığını garanti etti
                Email = user.Email!,
                PhoneNumber = request.PhoneNumber!.Trim(),
                Address = request.Address!.Trim(),
                Note = string.IsNullOrWhiteSpace(request.Note) ? null : request.Note.Trim(),
                AppUserId = user.Id,                        // sipariş AppUser'a bağlanıyor
                OrderItems = cart.Items.Select(x => new OrderItem // sepet satırları siparişe kopyalanıyor
                {
                    ProductId = x.ProductId,
                    ProductVariantId = x.VariantId,
                    ProductName = x.ProductName,
                    VariantName = x.VariantName,
                    ImageUrl = x.ImageUrl,
                    UnitPrice = x.UnitPrice,
                    Quantity = x.Quantity
                }).ToList()
            };

            // 6) PayTR'ye gönder (Adım 6'daki servis)
            var baseUrl = ResolveBaseUrl(httpContext);

            var redirectHtml = await _payTRService.CreatePaymentAsync(new PayTRPaymentRequest
            {
                OrderNo = orderNo,
                UserIp = GetClientIp(httpContext),
                Email = order.Email,
                PaymentAmount = paidPrice,
                InstallmentCount = request.InstallmentCount,
                CardType = cardBrand,
                CardOwner = request.CardOwner!.Trim(),
                CardNumber = cardNumber,
                ExpiryMonth = request.ExpiryMonth!.Trim().PadLeft(2, '0'), // "5" -> "05"
                ExpiryYear = request.ExpiryYear!.Trim(),
                Cvv = request.Cvv!.Trim(),
                UserName = order.FullName,
                UserAddress = order.Address,
                UserPhone = order.PhoneNumber,
                Basket = cart.Items
                    .Select(x => new PayTRBasketItem(
                        x.VariantName is null ? x.ProductName : $"{x.ProductName} ({x.VariantName})",
                        x.UnitPrice,
                        x.Quantity))
                    .ToList(),
                OkUrl = $"{baseUrl}/Payment/Success?orderNo={orderNo}",   // Adım 10'da yazılacak
                FailUrl = $"{baseUrl}/Payment/Failure?orderNo={orderNo}"
            }, cancellationToken);

            // 7) Buraya geldiysek PayTR isteği kabul etti -> siparişi "Pending" olarak kaydet
            await _orderRepository.CreateAsync(order);
            await _unitOfWork.SaveChangesAsync();

            // Sepet burada SİLİNMİYOR: ödeme 3D'de başarısız olabilir, kullanıcı tekrar denesin
            return new CreatePaymentResult
            {
                OrderNo = orderNo,
                RedirectHtml = redirectHtml
            };
        }


        // Session'daki fiyat, ürünün sepete eklendiği andaki fiyattır. Ödemeden hemen önce veritabanıyla karşılaştırıyoruz:
        // ürün silinmiş, seçenek kaldırılmış ya da fiyat değişmiş olabilir.
        // Değişiklik varsa sepeti güncelleyip kullanıcıyı uyarıyoruz; eski fiyattan ödeme alınmıyor.
        private async Task EnsureCartIsUpToDateAsync(Cart cart)
        {
            var productIds = cart.Items.Select(x => x.ProductId).Distinct().ToList();

            // Sepetteki bütün ürünler tek sorguda, varyantlarıyla birlikte
            var products = await _productRepository.GetQueryable()
                                                   .Include(x => x.ProductVariants)
                                                   .Where(x => productIds.Contains(x.Id))
                                                   .ToListAsync();

            var changed = false;

            // .ToList(): döngü içinde sepetten satır silebilmek için kopya üzerinde dönüyoruz
            foreach (var item in cart.Items.ToList())
            {
                var product = products.FirstOrDefault(x => x.Id == item.ProductId);
                var variant = item.VariantId is null
                    ? null
                    : product?.ProductVariants?.FirstOrDefault(x => x.Id == item.VariantId && x.IsAvailable);

                // Ürün silinmiş (soft delete filtresi getirmez) ya da seçenek artık yok
                if (product is null || (item.VariantId is not null && variant is null))
                {
                    cart.RemoveItem(item.ProductId, item.VariantId);
                    changed = true;
                    continue;
                }

                var currentPrice = product.Price + (variant?.AdditionalPrice ?? 0);
                if (currentPrice != item.UnitPrice)
                {
                    item.UnitPrice = currentPrice;
                    changed = true;
                }
            }

            // Kupon bu arada pasif yapıldıysa
            if (cart.CouponCode is not null &&
                await _couponRepository.GetActiveByCodeAsync(cart.CouponCode) is null)
            {
                cart.RemoveCoupon();
                changed = true;
            }

            if (changed)
            {
                _cartService.SaveCart(cart); // SaveCart içinde Recalculate çalışır
                throw new BusinessException("Sepetinizdeki bazı ürünlerin fiyatı veya durumu değişti, sepet güncellendi. Lütfen kontrol edip tekrar deneyin.");
            }
        }

        // secrets.json'da baseUrl (ngrok) varsa onu, yoksa isteğin geldiği adresi kullan
        private string ResolveBaseUrl(HttpContext httpContext)
        {
            var configured = _configuration["PayTR:baseUrl"];
            if (!string.IsNullOrWhiteSpace(configured))
                return configured.TrimEnd('/'); // sonda "/" varsa "//Payment" olmasın

            return $"{httpContext.Request.Scheme}://{httpContext.Request.Host}";
        }

        // PAY_TR'deki ile aynı: ngrok arkasında gerçek IP X-Forwarded-For başlığında gelir.
        // Birden fazla IP olabilir ("ip1, ip2"), ilki kullanıcınınkidir.
        private static string GetClientIp(HttpContext httpContext)
        {
            var forwarded = httpContext.Request.Headers["X-Forwarded-For"].FirstOrDefault();
            if (!string.IsNullOrWhiteSpace(forwarded))
                return forwarded.Split(',')[0].Trim();

            return httpContext.Connection.RemoteIpAddress?.ToString() ?? "127.0.0.1";
        }
    }
}