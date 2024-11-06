namespace Chocolatier.Domain.RequestFilter
{
    public class GetIngredientPaginationRequest : BasePaginationRequestFilter
    {
        public DateTime InitialExpirationDate { get; set; }
        public DateTime FinalExpirationDate { get; set; }
        public Guid IngredientTypeId { get; set; }

    }
}
