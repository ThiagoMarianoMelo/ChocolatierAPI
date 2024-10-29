namespace Chocolatier.Domain.Responses.DataResponses
{
    public class ProductListDataResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public double Price { get; set; }
        public int Quantity { get; set; }
        public Guid RecipeId { get; set; }
        public DateTime ExpireAt { get; set; }
    }
}
