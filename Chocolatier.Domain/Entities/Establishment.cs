using Chocolatier.Domain.Enum;
using Microsoft.AspNetCore.Identity;

namespace Chocolatier.Domain.Entities
{
    public class Establishment : IdentityUser
    {
       public Establishment() { }

        public string Address { get; set; } = string.Empty;
        public EstablishmentType EstablishmentType { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set;}

    }
}
