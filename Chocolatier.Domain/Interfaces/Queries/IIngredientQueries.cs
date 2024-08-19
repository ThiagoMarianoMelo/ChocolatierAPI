using Chocolatier.Domain.RequestFilter;
using Chocolatier.Domain.Responses;

namespace Chocolatier.Domain.Interfaces.Queries
{
    public interface IIngredientQueries
    {
        Task<Response> GetIngredientPagination(GetIngredientPaginationRequest request, CancellationToken cancellationToken);
    }
}
