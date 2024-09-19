using Chocolatier.Domain.Enum;

namespace Chocolatier.Domain.Entities
{
    public class Order : BaseEntity
    {
        public string RequestedById { get; set; } = string.Empty;
        public DateTime DeadLine { get; set; }
        public OrderStatus CurrentStatus { get; set; }
        public string? CancelReason { get; set; }
        public virtual Establishment? Establishment { get; set; }
    }
}
