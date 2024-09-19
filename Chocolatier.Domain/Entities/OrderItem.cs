namespace Chocolatier.Domain.Entities
{
    public class OrderItem : BaseEntity
    {
        public int Quantity { get; set; }
        public Guid OrderId { get; set; }
        public Guid RecipeId { get; set; }
        public virtual Order? Order { get; set; }
        public virtual Recipe? Recipe { get; set; }
    }
}
