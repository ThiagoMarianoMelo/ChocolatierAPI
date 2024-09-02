namespace Chocolatier.Domain.RequestFilter
{
    public class GetProductsPaginationRequest : BaseRequestFilter
    {
        public DateTime InitialExpiratonDate { get; set; }
        public DateTime FinalExpiratonDate { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}
