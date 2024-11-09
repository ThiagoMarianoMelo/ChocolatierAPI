using Chocolatier.Domain.RequestFilter;
using Chocolatier.Domain.Responses;

namespace Chocolatier.Domain.Interfaces.Queries
{
    public interface IReportQueries
    {
        Task<Response> GetCustomerPerDayReport(BaseReportRequestFilter requestFilter, CancellationToken cancellationToken);
        Task<Response> GetExpiringProductsPerDayReport(BaseReportRequestFilter requestFilter, CancellationToken cancellationToken);
        Task<Response> GetOrdersPerDayReport(GetOrderPerDayReportRequestFilter requestFilter, CancellationToken cancellationToken);
        Task<Response> GetCashCloseReport(GetCashClosePerDayReportRequestFilter requestFilter, CancellationToken cancellationToken);
    }
}
