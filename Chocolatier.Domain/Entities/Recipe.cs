namespace Chocolatier.Domain.Entities
{
    public class Recipe : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public int QuantityOfIngredients { get; set; }
        public bool IsActive { get; set; }
    }
}
