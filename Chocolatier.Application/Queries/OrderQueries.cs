using AutoMapper;
using Chocolatier.Domain.Entities;
using Chocolatier.Domain.Enum;
using Chocolatier.Domain.Interfaces;
using Chocolatier.Domain.Interfaces.Queries;
using Chocolatier.Domain.Interfaces.Repositories;
using Chocolatier.Domain.RequestFilter;
using Chocolatier.Domain.Responses;
using Chocolatier.Domain.Responses.DataResponses;
using System.Net;

namespace Chocolatier.Application.Queries
{
    public class OrderQueries : BasePaginationQueries<Order, OrdersListDataResponse>, IOrderQueries
    {
        private readonly IOrderRepository OrderRepository;
        private readonly IOrderItemRepository OrderItemRepository;
        private readonly IAuthEstablishment AuthEstablishment;
        private readonly IOrderHistoryRepository OrderHistoryRepository;

        public OrderQueries(IMapper mapper, IOrderRepository orderRepository, IOrderItemRepository orderItemRepository, IAuthEstablishment authEstablishment, IOrderHistoryRepository orderHistoryRepository) : base(mapper)
        {
            OrderRepository = orderRepository;
            OrderItemRepository = orderItemRepository;
            AuthEstablishment = authEstablishment;
            OrderHistoryRepository = orderHistoryRepository;
        }

        public async Task<Response> GetOrdersPagination(GetOrdersPaginationRequest request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.CurrentPage <= 0)
                    return new Response(false, "A página não pode ser anterior a pagina inicial 0.", HttpStatusCode.BadRequest);

                var queryableData = OrderRepository.GetQueryableOrdersFilter(request.OrderStatus, request.InitialDeadLineDate, request.FinalDeadLineDate,
                    request.InitialCreatedAtDate, request.FinalCreatedAtDate);

                var result = await BaseGetPaginantionDataByQueryable(queryableData, request, cancellationToken);

                return new Response(true, result, HttpStatusCode.OK);
            }
            catch (Exception)
            {
                return new Response(false, "Erro interno no processamento, entre em contato com o suporte.", HttpStatusCode.InternalServerError);
            }
        }

        public async Task<Response> GetOrderItens(Guid orderId, CancellationToken cancellationToken)
        {
            try
            {
                if (orderId == Guid.Empty)
                    return new Response(false, "Pedido informado inválido", HttpStatusCode.BadRequest);

                var requesterEstablishmentId = await OrderRepository.GetEstablishmentRequestFromOrder(orderId, cancellationToken);

                if (AuthEstablishment.EstablishmentType != EstablishmentType.Factory && requesterEstablishmentId != AuthEstablishment.Id)
                    return new Response(false, "Você não tem permissão para acessar os dados desse pedido", HttpStatusCode.BadRequest);

                var recipteItens = await OrderItemRepository.GetItensFromOrder(orderId, cancellationToken);

                var resultData = new List<OrderItensDataResponse>();

                recipteItens.ForEach(ri => resultData.Add(Mapper.Map<OrderItensDataResponse>(ri)));

                return new Response(true, resultData, HttpStatusCode.OK);
            }
            catch (Exception)
            {
                return new Response(false, "Erro interno no processamento, entre em contato com o suporte.", HttpStatusCode.InternalServerError);
            }
        }
        public async Task<Response> GetOrderHistory(Guid orderId, CancellationToken cancellationToken)
        {
            try
            {
                if (orderId == Guid.Empty)
                    return new Response(false, "Pedido informado inválido", HttpStatusCode.BadRequest);

                var requesterEstablishmentId = await OrderRepository.GetEstablishmentRequestFromOrder(orderId, cancellationToken);

                if (AuthEstablishment.EstablishmentType != EstablishmentType.Factory && requesterEstablishmentId != AuthEstablishment.Id)
                    return new Response(false, "Você não tem permissão para acessar os dados desse pedido", HttpStatusCode.BadRequest);

                var orderHistory = await OrderHistoryRepository.GetHistoryFromOrder(orderId, cancellationToken);

                var resultData = new List<OrderHistoryDataResponse>();

                orderHistory.ForEach(ri => resultData.Add(Mapper.Map<OrderHistoryDataResponse>(ri)));

                return new Response(true, resultData, HttpStatusCode.OK);
            }
            catch (Exception)
            {
                return new Response(false, "Erro interno no processamento, entre em contato com o suporte.", HttpStatusCode.InternalServerError);
            }
        }
    }
}
