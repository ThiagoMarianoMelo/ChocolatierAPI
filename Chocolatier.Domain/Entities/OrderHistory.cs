using Chocolatier.Domain.Enum;

namespace Chocolatier.Domain.Entities
{
    public class OrderHistory : BaseEntity
    {
        public Guid OrderId { get; set; }
        public EstablishmentType NewStatus { get; set; }
        public DateTime ChangedAt { get; set; }
        public virtual Order? Order { get; set; } 
    }
}
