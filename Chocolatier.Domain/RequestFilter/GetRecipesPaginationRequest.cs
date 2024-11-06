namespace Chocolatier.Domain.RequestFilter
{
    public class GetRecipesPaginationRequest : BasePaginationRequestFilter
    {
        public string Name { get; set; } = string.Empty;
    }
}
