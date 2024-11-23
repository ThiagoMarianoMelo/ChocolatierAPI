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
            var productsExpired = await ProductRepository.GetExpiredProductsCount(cancellationToken);
            var ingredientsExpired = await IngredientRepository.GetExpiredIngredientsCount(cancellationToken);
            var ordersNotConfirmed = await OrderRepository.GetOrderByStatusCount(OrderStatus.Pending, cancellationToken);
            var ordersDone = await OrderRepository.GetOrderByStatusCount(OrderStatus.Done, cancellationToken);


            var responseData = new GetHomeFactoryDataResponse
            {
                ProductsExpired = productsExpired,
                IngredientsExpired = ingredientsExpired,
                OrdersPending = ordersNotConfirmed,
                OrdersDone = ordersDone
            };

            return new Response(true, responseData, HttpStatusCode.OK);
        }
    }
}
