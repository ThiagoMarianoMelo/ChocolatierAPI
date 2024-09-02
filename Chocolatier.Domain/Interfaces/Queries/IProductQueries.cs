using Chocolatier.Domain.RequestFilter;
using Chocolatier.Domain.Responses;

namespace Chocolatier.Domain.Interfaces.Queries
{
    public interface IProductQueries
    {
        Task<Response> GetIngredientPagination(GetProductsPaginationRequest request, CancellationToken cancellationToken);
    }
}
