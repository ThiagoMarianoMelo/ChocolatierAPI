using Chocolatier.Domain.Enum;

namespace Chocolatier.Domain.Interfaces
{
    public interface IAuthEstablishment
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public EstablishmentType EstablishmentType { get; set; }    
        public string EstablishmentName { get; set; }
    }
}
