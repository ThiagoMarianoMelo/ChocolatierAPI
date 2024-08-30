using Chocolatier.Domain.Enum;

namespace Chocolatier.Domain.Responses.DataResponses
{
    public class RecipeItensDataResponse
    {
        public Guid Id { get; set; }
        public int Quantity { get; set; }
        public string IngredientTypeName { get; set; } = string.Empty;
        public MeasurementeUnit MeasurementeUnit { get; set; }
    }
}
