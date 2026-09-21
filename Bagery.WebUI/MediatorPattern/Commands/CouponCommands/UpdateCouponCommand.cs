using MediatR;

namespace Bagery.WebUI.MediatorPattern.Commands.CouponCommands
{
    public class UpdateCouponCommand : IRequest
    {
        public Guid Id { get; set; }
        public string? CouponCode { get; set; }
        public decimal? CouponPrice { get; set; }
        public decimal? MinPrice { get; set; }
        public bool IsActive { get; set; }
    }
}