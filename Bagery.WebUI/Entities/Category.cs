using Bagery.WebUI.Entities.Common;

namespace Bagery.WebUI.Entities
{
    public class Category : BaseEntity
    {
        public string CategoryName { get; set; }
        public IList<Product> Products { get; set; }

    }
}
