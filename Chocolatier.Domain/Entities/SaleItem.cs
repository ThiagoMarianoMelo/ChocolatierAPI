namespace Chocolatier.Domain.Entities
{
    public class SaleItem : BaseEntity
    {
        public int Quantity { get; set; }
        public Guid SaleId { get; set; }
        public Guid RecipeId { get; set; }
        public double UnityPrice { get; set; }
        public virtual Sale? Sale { get; set; }
        public virtual Recipe? Recipe { get; set; }
    }
}
