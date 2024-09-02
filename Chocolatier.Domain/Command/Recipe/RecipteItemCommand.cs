namespace Chocolatier.Domain.Command.Recipe
{
    public class RecipteItemCommand
    {
        public Guid IngredientTypeId { get; set; }
        public int Quantity { get; set; }
    }
}
