using Bagery.WebUI.Entities.Common;

namespace Bagery.WebUI.Entities
{
    public class Product:BaseEntity
    {
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string MainImageUrl { get; set; }
        public string ImageUrl1 { get; set; }
        public string ImageUrl2 { get; set; }
        public string PreperationDescription { get; set; }
        public Guid CategoryId { get; set; }
        public Category Category { get; set; }
        public IList<ProductVariant> ProductVariants { get; set; }

    }
}
