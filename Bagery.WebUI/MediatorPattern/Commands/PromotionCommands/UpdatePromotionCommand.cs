using MediatR;

namespace Bagery.WebUI.MediatorPattern.Commands.PromotionCommands
{
    public class UpdatePromotionCommand:IRequest
    {

        public Guid Id { get; set; }
        public string? ImageUrl { get; set; }
        public IFormFile? file { get; set; }
        public decimal? PromotionPrice { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? PromotionCode { get; set; }  

    }
}
