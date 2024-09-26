using Chocolatier.Domain.Enum;

namespace Chocolatier.Domain.RequestFilter
{
    public class GetOrdersPaginationRequest : BaseRequestFilter
    {
        public DateTime InitialDeadLineDate { get; set; }
        public DateTime FinalDeadLineDate { get; set; }
        public DateTime InitialCreatedAtDate { get; set; }
        public DateTime FinalCreatedAtDate { get; set; }
        public OrderStatus? OrderStatus { get; set; }
    }
}
