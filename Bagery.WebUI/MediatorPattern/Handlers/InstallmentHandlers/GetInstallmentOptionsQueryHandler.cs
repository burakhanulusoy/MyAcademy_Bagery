using Bagery.WebUI.Exceptions;
using Bagery.WebUI.MediatorPattern.Queries.InstallmentQueries;
using Bagery.WebUI.MediatorPattern.Results.InstallmentResults;
using Bagery.WebUI.Repositories.InstallmentRepositories;
using Bagery.WebUI.Services.CartServices;
using Bagery.WebUI.Services.PayTRServices;
using MediatR;

namespace Bagery.WebUI.MediatorPattern.Handlers.InstallmentHandlers
{
    public class GetInstallmentOptionsQueryHandler(IPayTRService _payTRService,
                                                   IInstallmentRepository _installmentRepository,
                                                   ICartService _cartService) : IRequestHandler<GetInstallmentOptionsQuery, List<GetInstallmentOptionsQueryResult>>
    {
        public async Task<List<GetInstallmentOptionsQueryResult>> Handle(GetInstallmentOptionsQuery request, CancellationToken cancellationToken)
        {
            // Boşlukları ve harfleri at, sadece rakamlar kalsın ("9792 0303" -> "97920303")
            var digits = new string((request.BinNumber ?? string.Empty).Where(char.IsDigit).ToArray());
            if (digits.Length < 8)
                throw new BusinessException("Taksit seçenekleri için kart numarasının ilk 8 hanesini girin.");

            // Tutarı istemci göndermiyor; sepetten alıyoruz
            var cart = _cartService.GetCart();
            if (cart.IsEmpty)
                throw new BusinessException("Sepetinizde ürün bulunmuyor.");

            // digits[..8] = ilk 8 karakter (fazlası geldiyse keser)
            var card = await _payTRService.GetCardInfoAsync(digits[..8], cancellationToken);

            // Tek çekim her kartta var (PAY_TR'deki gibi ilk satır)
            var options = new List<GetInstallmentOptionsQueryResult>
            {
                new() { InstallmentCount = 0, TotalPrice = cart.PaidPrice, MonthlyPrice = cart.PaidPrice }
            };

            // Taksit sadece kredi kartında; banka kartında liste tek çekimle kalır
            if (card.IsCreditCard)
            {
                var rates = await _installmentRepository.GetByBrandAsync(card.Brand); // ör. "world" oranları

                foreach (var rate in rates)
                {
                    var total = InstallmentCalculator.ApplyRate(cart.PaidPrice, rate.Rate);

                    options.Add(new GetInstallmentOptionsQueryResult
                    {
                        InstallmentCount = rate.InstallmentCount,
                        TotalPrice = total,
                        MonthlyPrice = InstallmentCalculator.MonthlyPrice(total, rate.InstallmentCount)
                    });
                }
            }

            return options;
        }
    }
}