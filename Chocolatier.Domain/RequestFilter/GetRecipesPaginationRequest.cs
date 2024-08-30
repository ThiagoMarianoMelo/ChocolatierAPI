namespace Chocolatier.Domain.RequestFilter
{
    public class GetRecipesPaginationRequest : BaseRequestFilter
    {
        public string Name { get; set; } = string.Empty;
    }
}
