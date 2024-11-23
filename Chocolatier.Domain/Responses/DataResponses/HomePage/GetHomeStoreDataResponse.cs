namespace Chocolatier.Domain.Responses.DataResponses.HomePage
{
    public class GetHomeStoreDataResponse
    {
        public int OrdersOnDelivery { get; set; } = 0;
        public int TotalOfSalesToday { get; set; } = 0;
        public double TotalBillingToday { get; set; } = 0;
        public int TotalOfNewCustomerThisWeek { get; set; } = 0;
    }
}
