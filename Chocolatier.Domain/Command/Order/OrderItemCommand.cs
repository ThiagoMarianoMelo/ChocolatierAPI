namespace Chocolatier.Domain.Command.Order
{
    public class OrderItemCommand
    {
        public int Quantity { get; set; }
        public Guid RecipeId { get; set; }
    }
}
