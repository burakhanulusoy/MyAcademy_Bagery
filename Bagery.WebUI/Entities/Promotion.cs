using Bagery.WebUI.Entities.Common;

namespace Bagery.WebUI.Entities
{
    public class Promotion:BaseEntity
    {

        public string ImageUrl { get; set; }
        public decimal PromotionPrice { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string PromotionCode { get; set; }


    }
}
