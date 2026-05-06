using Bagery.WebUI.Entities.Common;

namespace Bagery.WebUI.Entities
{
    public class ProductVariant:BaseEntity
    {
        public string OptionName { get; set; }
        public decimal AdditionalPrice { get; set; }
        public bool IsAvailable { get; set; } //hala devam ediyor mu ?

        public Guid ProductId { get; set; }
        public Product Product { get; set; }



    }
}
