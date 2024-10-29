namespace Chocolatier.Domain.Command.Sale
{
    public class SaleItemCommand
    {
        public Guid RecipeId { get; set; }
        public int Quantity { get; set; }
    }
}
