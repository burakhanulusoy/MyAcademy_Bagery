using MediatR;

namespace Bagery.WebUI.MediatorPattern.Commands.ProductCommands
{
    public class CreateProductCommand:IRequest
    {
        public string? ProductName { get; set; }
        public decimal? Price { get; set; }
        public string? Description { get; set; }
        public string? MainImageUrl { get; set; }
        public IFormFile? mainFile { get; set; }
        public string? ImageUrl1 { get; set; }
        public IFormFile? File1 { get; set; }
        public string? ImageUrl2 { get; set; }
        public IFormFile? File2 { get; set; }
        public string? PreperationDescription { get; set; }
        public Guid CategoryId { get; set; }
    }
}
