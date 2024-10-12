using Chocolatier.Domain.Enum;

namespace Chocolatier.Domain.Responses.DataResponses
{

    public class RecipeInfoListItensDataResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public List<RecipeItensDataResponse> Itens { get; set; } = [];
    }
    public class RecipeItensDataResponse
    {
        public Guid Id { get; set; }
        public int Quantity { get; set; }
        public string IngredientTypeName { get; set; } = string.Empty;
        public MeasurementeUnit MeasurementeUnit { get; set; }
    }
}
