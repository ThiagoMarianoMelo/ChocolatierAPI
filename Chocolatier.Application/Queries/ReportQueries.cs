using Chocolatier.Domain.Interfaces.Queries;
using Chocolatier.Domain.Interfaces.Repositories;
using Chocolatier.Domain.RequestFilter;
using Chocolatier.Domain.Responses;
using Chocolatier.Domain.Responses.DataResponses;
using System.Net;

namespace Chocolatier.Application.Queries
{
    public class ReportQueries : IReportQueries
    {
        private readonly ICustomerRepository CustomerRepository;

        public ReportQueries(ICustomerRepository customerRepository)
        {
            CustomerRepository = customerRepository;
        }

        public async Task<Response> GetCustomerPerDayReport(BaseReportRequestFilter requestFilter, CancellationToken cancellationToken)
        {
            var data = await CustomerRepository.GetNewCustomerByIntervalBasedOnDay(requestFilter.StartDate, requestFilter.EndDate, cancellationToken);

            var reportData = data.GroupBy(c => c.CreatedAt.Date)
                              .Select(group => new DataPerDay
                              {
                                  Date = group.Key,
                                  Amount = group.Count()
                              }).ToList();

            return new Response(true, new BaseReportResponse { ReportData = reportData }, HttpStatusCode.OK);
        }
    }
}
