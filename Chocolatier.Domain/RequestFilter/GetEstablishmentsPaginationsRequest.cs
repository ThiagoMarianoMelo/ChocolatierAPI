namespace Chocolatier.Domain.RequestFilter
{
    public class GetEstablishmentsPaginationsRequest : BaseRequestFilter
    {
        public string Name { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;
    }
}
