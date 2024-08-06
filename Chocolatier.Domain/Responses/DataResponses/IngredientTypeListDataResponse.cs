using Chocolatier.Domain.Enum;

namespace Chocolatier.Domain.Responses.DataResponses
{
    public class IngredientTypeListDataResponse
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;

        public MeasurementeUnit MeasurementeUnit { get; set; }
    }
}
