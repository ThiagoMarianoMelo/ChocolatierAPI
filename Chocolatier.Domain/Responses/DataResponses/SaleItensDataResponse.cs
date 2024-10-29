namespace Chocolatier.Domain.Responses.DataResponses
{
    public class SaleItensDataResponse
    {
        public Guid RecipeId { get; set; }
        public int Quantity { get; set; }
        public double UnityPrice { get; set; }
        public string RecipeName { get; set; } = string.Empty;
    }
}
