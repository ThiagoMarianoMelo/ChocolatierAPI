using Chocolatier.Domain.Entities;

namespace Chocolatier.Domain.Interfaces.Repositories
{
    public interface IEstablishmentRepository
    {
        IQueryable<Establishment> GetQueryableEstablishmentsByFilter(string name, string email);
        Task<List<string?>> GetFactoryEmails(CancellationToken cancellationToken);
    }
}
