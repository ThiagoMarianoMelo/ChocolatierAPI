using AutoMapper;
using Chocolatier.Domain.Enum;
using Chocolatier.Domain.Interfaces.Queries;
using Chocolatier.Domain.Interfaces.Repositories;
using Chocolatier.Domain.Responses;
using Chocolatier.Domain.Responses.DataResponses;
using Chocolatier.Domain.Responses.DataResponses.HomePage;
using System.Net;

namespace Chocolatier.Application.Queries
{
    public class HomePageQueries : IHomePageQueries
    {
        private readonly IProductRepository ProductRepository;
        private readonly IIngredientRepository IngredientRepository;
        private readonly IOrderRepository OrderRepository;
        private readonly IMapper Mapper;

        public HomePageQueries(IProductRepository productRepository, IIngredientRepository ingredientRepository, IOrderRepository orderRepository, IMapper mapper)
        {
            ProductRepository = productRepository;
            IngredientRepository = ingredientRepository;
            OrderRepository = orderRepository;
            Mapper = mapper;
        }

        public async Task<Response> GetHomeFactoryData(CancellationToken cancellationToken)
        {
            var productsExpired = await ProductRepository.GetExpiredProducts(10, cancellationToken);
            var ingredientsExpired = await IngredientRepository.GetExpiredIngredients(10, cancellationToken);
            var ordersNotConfirmed = await OrderRepository.GetOrderByStatusAndRegisterRange(10, OrderStatus.Pending, cancellationToken);
            var orderDoneReportData = await OrderRepository.GetOrdersByDeadLineAndStatus(DateTime.UtcNow.AddDays(-7), DateTime.UtcNow, OrderStatus.Done, cancellationToken);

            var ordeDoneReportData = orderDoneReportData
                                    .OrderBy(c => c.CreatedAt.Date)
                                    .GroupBy(c => c.CreatedAt.Date)
                                      .Select(group => new DataPerDay<int>
                                      {
                                          Date = group.Key,
                                          Amount = group.Count()
                                      })
                                      .ToList();

            var responseData = new GetHomeFactoryDataResponse
            {
                ProductsExpired = Mapper.Map<List<ProductListDataResponse>>(productsExpired),
                IngredientsExpired = Mapper.Map<List<IngredientListDataResponse>>(ingredientsExpired),
                OrdersPending = Mapper.Map<List<OrdersListDataResponse>>(ordersNotConfirmed),
                OrdersDoneReportData = ordeDoneReportData
            };

            return new Response(true, responseData, HttpStatusCode.OK);
        }

    }
}
