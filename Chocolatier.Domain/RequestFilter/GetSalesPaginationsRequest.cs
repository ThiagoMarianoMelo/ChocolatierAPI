using Chocolatier.Domain.Enum;

namespace Chocolatier.Domain.RequestFilter
{
    public class GetSalesPaginationsRequest : BasePaginationRequestFilter
    {
        public PaymentMethod? PaymentMethod { get; set; }
        public Guid SaleId { get; set; }
        public DateTime InitialCreatedAtDate { get; set; }
        public DateTime FinalCreatedAtDate { get; set; }
    }
}
