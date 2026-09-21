using MediatR;

namespace Bagery.WebUI.MediatorPattern.Commands.CouponCommands
{
    public class CreateCouponCommand : IRequest
    {
        public string? CouponCode { get; set; }
        public decimal? CouponPrice { get; set; }   // indirim tutarı (₺)
        public decimal? MinPrice { get; set; }      // boş bırakılırsa 0 = şart yok
        public bool IsActive { get; set; } = true;  // yeni kupon varsayılan aktif
    }
}