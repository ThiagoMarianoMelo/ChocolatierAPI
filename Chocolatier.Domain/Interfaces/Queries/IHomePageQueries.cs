using Chocolatier.Domain.Responses;

namespace Chocolatier.Domain.Interfaces.Queries
{
    public interface IHomePageQueries
    {
        Task<Response> GetHomeFactoryData(CancellationToken cancellationToken);
    }
}
