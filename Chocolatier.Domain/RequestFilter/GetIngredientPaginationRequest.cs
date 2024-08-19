namespace Chocolatier.Domain.RequestFilter
{
    public class GetIngredientPaginationRequest : BaseRequestFilter
    {
        public DateTime InitialDate { get; set; }
        public DateTime FinalDate { get; set; }
        public Guid IngredientTypeId { get; set; }

    }
}
