using Chocolatier.Domain.Entities;

namespace Chocolatier.Domain.Responses.DataResponses
{
    public class IngredientListDataResponse
    {
        public Guid Id { get; set; }
        public DateTime ExpireAt { get; set; }
        public IngredientType? IngredientType { get; set; }
    }
}
