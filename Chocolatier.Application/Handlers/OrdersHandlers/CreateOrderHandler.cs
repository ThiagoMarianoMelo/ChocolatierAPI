using AutoMapper;
using Chocolatier.Domain.Command.Order;
using Chocolatier.Domain.Entities;
using Chocolatier.Domain.Enum;
using Chocolatier.Domain.Interfaces;
using Chocolatier.Domain.Interfaces.Repositories;
using Chocolatier.Domain.Interfaces.Senders;
using Chocolatier.Domain.Responses;
using MediatR;
using System.Net;

namespace Chocolatier.Application.Handlers.OrdersHandlers
{
    public class CreateOrderHandler : BaseOrderHandler, IRequestHandler<CreateOrderCommand, Response>
    {

        private readonly IRecipeRepository RecipeRepository;
        private readonly IMapper Mapper;
        private readonly IAuthEstablishment AuthEstablishment;
        private readonly IOrderRepository OrderRepository;
        private readonly IOrderItemRepository OrderItemRepository;
        private readonly IOrderHistoryRepository OrderHistoryRepository;
        private readonly IEstablishmentRepository EstablishmentRepository;

        public CreateOrderHandler(IRecipeRepository recipeRepository, IMapper mapper, IAuthEstablishment authEstablishment, 
            IOrderRepository orderRepository, IOrderItemRepository orderItemRepository, IOrderHistoryRepository orderHistoryRepository, 
            IEmailQueueSender emailQueueSender, IEstablishmentRepository establishmentRepository): base(emailQueueSender)
        {
            RecipeRepository = recipeRepository;
            Mapper = mapper;
            AuthEstablishment = authEstablishment;
            OrderRepository = orderRepository;
            OrderItemRepository = orderItemRepository;
            OrderHistoryRepository = orderHistoryRepository;
            EstablishmentRepository = establishmentRepository;
        }

        public async Task<Response> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            try
            {
                request.Validate();
                if (!request.IsValid)
                    return new Response(false, request.Notifications);

                var recipesVerifiyResponse = await VerifyRecipesAreAvaibles(request.OrderItens!.Select(orItens => orItens.RecipeId), cancellationToken);

                if (!recipesVerifiyResponse.Success)
                    return recipesVerifiyResponse;

                var order = Mapper.Map<Order>(request);

                order.RequestedById = AuthEstablishment.Id;
                order.CurrentStatus = OrderStatus.Pending;
                order.DeadLine = request.DeadLine.ToUniversalTime();
                order.CreatedAt = DateTime.UtcNow;

                var orderResult = await OrderRepository.Create(order, cancellationToken);

                if (orderResult is null)
                    return new Response(true, ["Erro ao cadastrar pedido."], HttpStatusCode.InternalServerError);

                var orderItens = Mapper.Map<List<OrderItem>>(request.OrderItens);

                orderItens.ForEach(orItem => orItem.OrderId = orderResult.Id);

                orderItens.ForEach(async orItem => await OrderItemRepository.Create(orItem, cancellationToken));

                await OrderHistoryRepository.Create(new OrderHistory
                {
                    OrderId = orderResult.Id,
                    NewStatus = OrderStatus.Pending,
                    ChangedAt = DateTime.UtcNow

                }, cancellationToken);

                var result = await RecipeRepository.SaveChanges(cancellationToken);

                if (result <= 0)
                    return new Response(true, ["Erro ao cadastrar pedido, entre em contato com o suporte."], HttpStatusCode.InternalServerError);

                var emailsToSendNotify = await EstablishmentRepository.GetFactoryEmails(cancellationToken);

                emailsToSendNotify.Add(AuthEstablishment.Email);

                var emailParams = GetEmailParamsOrderCreated(orderResult);

                _ = SendEmailOrder(emailsToSendNotify!, EmailTemplate.OrderCreated, emailParams);

                return new Response(true, orderResult.Id, HttpStatusCode.Created);
            }
            catch (Exception)
            {
                return new Response(false, "Erro interno no processamento, entre em contato com o suporte.", HttpStatusCode.InternalServerError);
            }
        }

        private async Task<Response> VerifyRecipesAreAvaibles(IEnumerable<Guid> recipesIds, CancellationToken cancellationToken)
        {
            foreach (var recipeId in recipesIds)
            {
                var isActive = await RecipeRepository.IsActiveById(recipeId, cancellationToken);

                if (!isActive) return new Response(false, [$"A receita {recipeId} não está ativa, entre em contato com o suporte"], HttpStatusCode.BadRequest);
            }

            if (recipesIds.Count() != recipesIds.Distinct().Count())
                return new Response(false, ["Existem itens duplicados no pedido."], HttpStatusCode.BadRequest);

            return new Response(true);
        }

        private Dictionary<string,string> GetEmailParamsOrderCreated(Order order) 
            => new()
            {
                { "[ORDERID]", order.Id.ToString() },
                { "[EstablishmentName]", AuthEstablishment.EstablishmentName },
                { "[ORDERDEADLINE]", order.DeadLine.ToString("dd/MM/yyyy")}
            };
    }
}
