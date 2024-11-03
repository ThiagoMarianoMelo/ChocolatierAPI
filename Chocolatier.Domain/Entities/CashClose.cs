
namespace Chocolatier.Domain.Entities
{
    public class CashClose : BaseEntity
    {
        public int SaleQuantity { get; set; }
        public double Billing {  get; set; }
        public DateTime Date {  get; set; }
        public string EstablishmentId { get; set; } = string.Empty;
        public virtual Establishment? Establishment {  get; set; } 
    }
}