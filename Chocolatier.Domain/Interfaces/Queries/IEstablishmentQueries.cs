using Chocolatier.Domain.RequestFilter;
using Chocolatier.Domain.Responses;

namespace Chocolatier.Domain.Interfaces.Queries
{
    public interface IEstablishmentQueries
    {
        Task<Response> GetEstablishmentsPaginations(GetEstablishmentsPaginationsRequest request, CancellationToken cancellationToken);
    }
}
