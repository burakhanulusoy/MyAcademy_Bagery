using Bagery.WebUI.MediatorPattern.Results.PaymentResults;
using MediatR;

namespace Bagery.WebUI.MediatorPattern.Commands.PaymentCommands
{
    // Tutar ve ürün bilgisi burada YOK: sepet session'dan, fiyatlar veritabanından gelir.
    // PAY_TR'deki PaymentCreateModel'in karşılığı + teslimat bilgileri
    public class CreatePaymentCommand : IRequest<CreatePaymentResult>
    {
        // Teslimat
        public string? FullName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
        public string? Note { get; set; }

        // Kart (PAY_TR'de CartOwner/CartNumber yazılmıştı; "Card" olarak düzelttik)
        public string? CardOwner { get; set; }
        public string? CardNumber { get; set; }
        public string? ExpiryMonth { get; set; }
        public string? ExpiryYear { get; set; }
        public string? Cvv { get; set; }
        public int InstallmentCount { get; set; }   // 0 = tek çekim (seçilmezse varsayılan 0)
    }
}