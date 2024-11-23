namespace Chocolatier.Domain.Responses.DataResponses.HomePage
{
    public class GetHomeFactoryDataResponse
    {
        public List<ProductListDataResponse> ProductsExpired { get; set; } = new List<ProductListDataResponse>();
        public List<IngredientListDataResponse> IngredientsExpired { get; set; } = new List<IngredientListDataResponse>();
        public List<OrdersListDataResponse> OrdersPending { get; set; } = new List<OrdersListDataResponse>();
        public List<DataPerDay<int>>? OrdersDoneReportData { get; set; }
    }
}
