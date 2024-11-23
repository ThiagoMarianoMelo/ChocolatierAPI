namespace Chocolatier.Domain.Responses.DataResponses.HomePage
{
    public class GetHomeFactoryDataResponse
    {
        public int ProductsExpired { get; set; } = 0;
        public int IngredientsExpired { get; set; } = 0;
        public int OrdersPending { get; set; } = 0;
        public int OrdersDone { get; set; } = 0;
    }
}
