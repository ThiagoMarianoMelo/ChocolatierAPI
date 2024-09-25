using Chocolatier.Domain.RequestFilter;
using Chocolatier.Domain.Responses;

namespace Chocolatier.Domain.Interfaces.Queries
{
    public interface IOrderQueries
    {
        Task<Response> GetOrdersPagination(GetOrdersPaginationRequest request, CancellationToken cancellationToken);
    }
}
