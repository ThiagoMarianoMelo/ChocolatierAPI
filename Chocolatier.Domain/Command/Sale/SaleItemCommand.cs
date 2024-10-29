namespace Chocolatier.Domain.Command.Sale
{
    public class SaleItemCommand
    {
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
