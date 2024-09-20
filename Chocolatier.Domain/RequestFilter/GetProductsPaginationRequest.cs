namespace Chocolatier.Domain.RequestFilter
{
    public class GetProductsPaginationRequest : BaseRequestFilter
    {
        public DateTime InitialExpirationDate { get; set; }
        public DateTime FinalExpirationDate { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}
