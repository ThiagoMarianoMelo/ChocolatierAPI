using Chocolatier.Domain.Enum;

namespace Chocolatier.Domain.Responses.DataResponses
{
    public class OrderHistoryDataResponse
    {
        public OrderStatus NewStatus { get; set; }
        public DateTime ChangedAt { get; set; }
    }
}
