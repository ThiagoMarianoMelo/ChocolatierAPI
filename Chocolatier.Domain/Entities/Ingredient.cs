namespace Chocolatier.Domain.Entities
{
    public class Ingredient : BaseEntity
    {
        public Guid IngredientTypeId { get; set; }
        public int Amount { get; set; }
        public DateTime ExpireAt { get; set; }
        public virtual IngredientType? IngredientType { get; set; }
    }
}
