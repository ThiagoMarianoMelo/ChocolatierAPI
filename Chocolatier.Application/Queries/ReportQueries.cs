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
        private readonly ICashCloseRepository CashCloseRepository;

        public ReportQueries(ICustomerRepository customerRepository, IProductRepository productRepository, IOrderRepository orderRepository, ICashCloseRepository cashCloseRepository)
        {
            CustomerRepository = customerRepository;
            ProductRepository = productRepository;
            OrderRepository = orderRepository;
            CashCloseRepository = cashCloseRepository;
        }

        public async Task<Response> GetCustomerPerDayReport(BaseReportRequestFilter requestFilter, CancellationToken cancellationToken)
        {
            var data = await CustomerRepository.GetNewCustomerByIntervalBasedOnDay(requestFilter.StartDate, requestFilter.EndDate, cancellationToken);

            return PrepareReportReponseDataPerDayBasedOnCount(data, c => c.CreatedAt.Date);
        }

        public async Task<Response> GetExpiringProductsPerDayReport(BaseReportRequestFilter requestFilter, CancellationToken cancellationToken)
        {
            var data = await ProductRepository.GetExpiringProductsBasedOnDateFilter(requestFilter.StartDate, requestFilter.EndDate, cancellationToken);

            return PrepareReportReponseDataPerDayBasedOnCount(data, c => c.ExpireAt.Date);
        }

        public async Task<Response> GetOrdersPerDayReport(GetOrderPerDayReportRequestFilter requestFilter, CancellationToken cancellationToken)
        {
            var data = await OrderRepository.GetOrdersByDeadLineAndStatus(requestFilter.StartDate, requestFilter.EndDate, requestFilter.OrderStatus, cancellationToken);

            return PrepareReportReponseDataPerDayBasedOnCount(data, c => c.CreatedAt.Date);
        }
        public async Task<Response> GetCashCloseReport(GetCashClosePerDayReportRequestFilter requestFilter, CancellationToken cancellationToken)
        {
            var data = await CashCloseRepository.GetCashCloseByDataFilter(requestFilter.StartDate, requestFilter.EndDate, cancellationToken);

            if (requestFilter.ReportType == CashClosReportType.ByMoney)
            {
                var reportData = data.OrderBy(c => c.Date.Date)
                    .GroupBy(c => c.Date.Date)
                    .Select(group => new DataPerDay<double>
                    {
                        Date = group.Key,
                        Amount = group.Sum(c => c.Billing)
                    }).ToList();

                return new Response(true, new BaseReportResponse<double> { ReportData = reportData }, HttpStatusCode.OK);
            }
            else
            {
                var reportData = data.OrderBy(c => c.Date.Date)
                    .GroupBy(c => c.Date.Date)
                    .Select(group => new DataPerDay<int>
                    {
                        Date = group.Key,
                        Amount = group.Sum(c => c.SaleQuantity)
                    }).ToList();

                return new Response(true, new BaseReportResponse<int> { ReportData = reportData }, HttpStatusCode.OK);
            }
        }


        private Response PrepareReportReponseDataPerDayBasedOnCount<TData>(List<TData> data, Func<TData, DateTime> orderByGroupByFunc)
        {
            var reportData = data.OrderBy(orderByGroupByFunc)
                  .GroupBy(orderByGroupByFunc)
                  .Select(group => new DataPerDay<int>
                  {
                      Date = group.Key,
                      Amount = group.Count()
                  })
                  .ToList();

            return new Response(true, new BaseReportResponse<int> { ReportData = reportData }, HttpStatusCode.OK);
        }

    }
}
