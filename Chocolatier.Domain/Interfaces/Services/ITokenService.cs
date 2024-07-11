using Chocolatier.Domain.Entities;

namespace Chocolatier.Domain.Interfaces.Services
{
    public interface ITokenService
    {
        Task<string> GenerateTokenByEstablishment(Establishment establishment);
    }
}
