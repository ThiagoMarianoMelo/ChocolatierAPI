using System.Text.Json.Serialization;

namespace Chocolatier.Domain.Entities
{
    public class RecipeItem : BaseEntity
    {
        public int Quantity { get; set; }
        public Guid RecipeId { get; set; }
        public Guid IngredientTypeId { get; set; }
        public virtual Recipe Recipe { get; set; } = new Recipe();
        public virtual IngredientType IngredientType { get; set; } = new IngredientType();
    }
}
