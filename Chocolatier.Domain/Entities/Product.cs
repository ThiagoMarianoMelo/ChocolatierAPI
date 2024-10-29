namespace Chocolatier.Domain.Entities
{
    public class Product : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public double Price { get; set; }
        public Guid RecipeId { get; set; }
        public int Quantity { get; set; }
        public string CurrentEstablishmentId { get; set; } = string.Empty;
        public DateTime ExpireAt { get; set; }
        public virtual Establishment? Establishment { get; set; }
        public virtual Recipe? Recipe { get; set; }
    }
}
