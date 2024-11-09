using Chocolatier.Domain.Enum;

namespace Chocolatier.Domain.RequestFilter
{
    public class GetOrderPerDayReportRequestFilter : BaseReportRequestFilter
    {
        public OrderStatus? OrderStatus { get; set; }
    }
}