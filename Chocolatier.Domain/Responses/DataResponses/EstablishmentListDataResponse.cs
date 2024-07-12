using Chocolatier.Domain.Enum;

namespace Chocolatier.Domain.Responses.DataResponses
{
    public class EstablishmentListDataResponse
    {
        public string UserName { get; set; } = string.Empty;

        public EstablishmentType EstablishmentType { get; set; }

        public string Email { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;

        public string Address { get; set; } = string.Empty;
    }
}
