using Chocolatier.Domain.Entities;
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
        private readonly IProductRepository ProductRepository;
        private readonly IOrderRepository OrderRepository;

        public ReportQueries(ICustomerRepository customerRepository, IProductRepository productRepository, IOrderRepository orderRepository)
        {
            CustomerRepository = customerRepository;
            ProductRepository = productRepository;
            OrderRepository = orderRepository;
        }

        public async Task<Response> GetCustomerPerDayReport(BaseReportRequestFilter requestFilter, CancellationToken cancellationToken)
        {
            var data = await CustomerRepository.GetNewCustomerByIntervalBasedOnDay(requestFilter.StartDate, requestFilter.EndDate, cancellationToken);

            return PrepareReportReponseDataPerDay(data, c => c.CreatedAt.Date);
        }

        public async Task<Response> GetExpiringProductsPerDayReport(BaseReportRequestFilter requestFilter, CancellationToken cancellationToken)
        {
            var data = await ProductRepository.GetExpiringProductsBasedOnDateFilter(requestFilter.StartDate, requestFilter.EndDate, cancellationToken);

            return PrepareReportReponseDataPerDay(data, c => c.ExpireAt.Date);
        }

        public async Task<Response> GetOrdersPerDayReport(GetOrderPerDayReportRequestFilter requestFilter, CancellationToken cancellationToken)
        {
            var data = await OrderRepository.GetOrdersByDeadLineAndStatus(requestFilter.StartDate, requestFilter.EndDate, requestFilter.OrderStatus, cancellationToken);

            return PrepareReportReponseDataPerDay(data, c => c.CreatedAt.Date);
        }

        private Response PrepareReportReponseDataPerDay<TData>(List<TData> data, Func<TData, DateTime> orderByFunc)
        {
            var reportData = data.GroupBy(orderByFunc)
                  .Select(group => new DataPerDay
                  {
                      Date = group.Key,
                      Amount = group.Count()
                  }).ToList();

            return new Response(true, new BaseReportResponse { ReportData = reportData }, HttpStatusCode.OK);
        }

    }
}
