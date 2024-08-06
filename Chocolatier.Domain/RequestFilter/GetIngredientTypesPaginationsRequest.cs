namespace Chocolatier.Domain.RequestFilter
{
    public class GetIngredientTypesPaginationsRequest : BaseRequestFilter
    {
        public string Name { get; set; } = string.Empty;
    }
}
