namespace Chocolatier.Domain.Responses.DataResponses
{
    public class RecipesListDataResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int QuantityOfIngredients { get; set; }
    }
}
