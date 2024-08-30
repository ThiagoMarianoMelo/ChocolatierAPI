using Chocolatier.Domain.RequestFilter;
using Chocolatier.Domain.Responses;

namespace Chocolatier.Domain.Interfaces.Queries
{
    public interface IRecipeQueries
    {
        Task<Response> GetRecipesPagination(GetRecipesPaginationRequest request, CancellationToken cancellationToken);
    }
}
