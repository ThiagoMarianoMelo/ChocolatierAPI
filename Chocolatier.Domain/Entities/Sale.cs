using Chocolatier.Domain.Enum;

namespace Chocolatier.Domain.Entities
{
    public class Sale : BaseEntity
    {
        public double TotalPrice { get; set; }
        public Guid CustomerId { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public string? EstablishmentId { get; set; }
        public DateTime SaleDate { get; set; }
        public virtual Establishment? Establishment { get; set; }
        public virtual Customer? Customer { get; set; }
    }
}
