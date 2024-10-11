using Chocolatier.Domain.Responses;

namespace Chocolatier.Domain.Interfaces.Queries
{
    public interface ICustomerQueries
    {
        Task<Response> GetCustomersList(CancellationToken cancellationToken);
    }
}
