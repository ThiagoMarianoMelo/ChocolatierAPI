namespace Chocolatier.Domain.Responses.DataResponses
{
    public class ProductListDataResponse
    {
        public string Name { get; set; } = string.Empty;
        public double Price { get; set; }
        public Guid RecipeId { get; set; }
        public DateTime ExpireAt { get; set; }
    }
}
