using Bagery.WebUI.Entities.Common;

namespace Bagery.WebUI.Entities
{
    public class Installment:BaseEntity
    {
        public string Brand { get; set; }            // world, bonus, maximum, axess... marka 
        public int InstallmentCount { get; set; }
        public decimal Rate { get; set; }            // yüzde, ör. 2.45 taksit oranı 
    }
}
