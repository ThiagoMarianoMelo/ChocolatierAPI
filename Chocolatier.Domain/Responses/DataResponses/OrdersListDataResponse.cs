using Chocolatier.Domain.Enum;

namespace Chocolatier.Domain.Responses.DataResponses
{
    public class OrdersListDataResponse
    {
        public Guid Id { get; set; }
        public OrderStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime DeadLine { get; set; }
    }
}
