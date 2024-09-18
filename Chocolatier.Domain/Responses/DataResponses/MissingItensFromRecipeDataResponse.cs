using Chocolatier.Domain.Enum;

namespace Chocolatier.Domain.Responses.DataResponses
{
    public class MissingItensFromRecipeDataResponse
    {
        public int MissingAmout { get; set; }
        public MeasurementeUnit MeasurementeUnit { get; set; }  
        public string IngredientName { get; set; } = string.Empty;
    }
}
