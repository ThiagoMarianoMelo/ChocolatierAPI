namespace Chocolatier.Domain.Responses.DataResponses
{
    public class OrderItensDataResponse
    {
        public Guid RecipeId { get; set; }
        public int Quantity { get; set; }
        public string RecipeName { get; set; } = string.Empty;
    }
}
