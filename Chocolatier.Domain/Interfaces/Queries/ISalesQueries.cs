using Chocolatier.Domain.RequestFilter;
using Chocolatier.Domain.Responses;

namespace Chocolatier.Domain.Interfaces.Queries
{
    public interface ISalesQueries
    {
        Task<Response> GetSalesPagination(GetSalesPaginationsRequest request, CancellationToken cancellationToken);
        Task<Response> GetSaleItens(Guid saleId, CancellationToken cancellationToken);
    }
}
