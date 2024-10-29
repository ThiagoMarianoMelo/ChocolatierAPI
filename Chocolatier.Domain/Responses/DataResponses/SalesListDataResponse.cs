using Chocolatier.Domain.Enum;

namespace Chocolatier.Domain.Responses.DataResponses
{
    public class SalesListDataResponse
    {
        public Guid Id { get; set; }
        public double TotalPrice { get; set; }
        public Guid CustomerId { get; set; }
        public PaymentMethod PaymentMethod {  get; set; }
        public DateTime SaleDate { get; set; }
    }
}
