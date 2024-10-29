namespace Chocolatier.Domain.Entities
{
    public class SaleItem : BaseEntity
    {
        public int Quantity { get; set; }
        public Guid SaleId { get; set; }
        public Guid ProductId { get; set; }
        public virtual Sale? Sale { get; set; }
        public virtual Product? Product { get; set; }
    }
}
