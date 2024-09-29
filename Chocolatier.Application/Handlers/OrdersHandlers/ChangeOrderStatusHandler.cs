using Chocolatier.Domain.Command.Order;
using Chocolatier.Domain.Entities;
using Chocolatier.Domain.Enum;
using Chocolatier.Domain.Interfaces;
using Chocolatier.Domain.Interfaces.Repositories;
using Chocolatier.Domain.Interfaces.Senders;
using Chocolatier.Domain.Responses;
using Chocolatier.Util.EnumUtil;
using MediatR;
using System.Net;

namespace Chocolatier.Application.Handlers.OrdersHandlers
{
    public class ChangeOrderStatusHandler : BaseOrderHandler, IRequestHandler<ChangeOrderStatusCommand, Response>
    {
        private readonly IOrderRepository OrderRepository;
        private readonly IOrderHistoryRepository OrderHistoryRepository;
        private readonly IEstablishmentRepository EstablishmentRepository;
        private readonly IAuthEstablishment AuthEstablishment;

        public ChangeOrderStatusHandler(IOrderRepository orderRepository, IOrderHistoryRepository orderHistoryRepository,
            IEmailQueueSender emailQueueSender, IEstablishmentRepository establishmentRepository, IAuthEstablishment authEstablishment) : base(emailQueueSender)
        {
            OrderRepository = orderRepository;
            OrderHistoryRepository = orderHistoryRepository;
            EstablishmentRepository = establishmentRepository;
            AuthEstablishment = authEstablishment;
        }

        public async Task<Response> Handle(ChangeOrderStatusCommand request, CancellationToken cancellationToken)
        {
            request.Validate();
            if (!request.IsValid)
                return new Response(false, request.Notifications);

            var order = await OrderRepository.GetEntityById(request.Id, cancellationToken);

            if (order is null)
                return new Response(true, ["Erro ao encontrar pedido."], HttpStatusCode.InternalServerError);

            var oldStauts = order.CurrentStatus;

            var statusCanBeChanged = VerifyStatusCanBeChanged(order, request.NewStatus);

            if (!statusCanBeChanged)
                return new Response(true, [$"O status {order.CurrentStatus} não pode ser alterado para {request.NewStatus}."], HttpStatusCode.InternalServerError);

            var orderHistoryResult = await OrderHistoryRepository.Create(new OrderHistory
            {
                OrderId = order.Id,
                NewStatus = request.NewStatus,
                ChangedAt = DateTime.UtcNow
            }, cancellationToken);

            if (orderHistoryResult is null)
                return new Response(true, ["Erro ao alterar pedido."], HttpStatusCode.InternalServerError);

            order.CurrentStatus = request.NewStatus;

            var resultUpdateOrder = OrderRepository.UpdateEntity(order, cancellationToken);

            if (resultUpdateOrder is null)
                return new Response(true, ["Erro ao alterar pedido."], HttpStatusCode.InternalServerError);

            await OrderRepository.SaveChanges(cancellationToken);

            var emailsToSendNotify = await EstablishmentRepository.GetFactoryEmails(cancellationToken);

            emailsToSendNotify.Add(order.Establishment?.Email);

            var emailParams = GetEmailParamsOrderCreated(order, oldStauts!);

            _ = SendEmailOrder(emailsToSendNotify!, EmailTemplate.OrderCreated, emailParams);

            return new Response(true, HttpStatusCode.Created);
        }

        private static bool VerifyStatusCanBeChanged(Order order, OrderStatus newOrderStatus) => newOrderStatus switch
        {
            OrderStatus.Confirmed => order.CurrentStatus == OrderStatus.Pending,
            OrderStatus.OnPrepare => order.CurrentStatus == OrderStatus.Confirmed,
            OrderStatus.OnDelivery => order.CurrentStatus == OrderStatus.OnPrepare,
            OrderStatus.Done => order.CurrentStatus == OrderStatus.OnDelivery,
            _ => false
        };

        private Dictionary<string, string> GetEmailParamsOrderCreated(Order order, OrderStatus oldStauts)
        => new()
        {
            { "[ORDERID]", order.Id.ToString() },
            { "[EstablishmentName]", AuthEstablishment.EstablishmentName },
            { "[STATUSANTIGO]", oldStauts.GetStringValue()},
            { "[NOVOSTATUS]", order.CurrentStatus.GetStringValue()}
        };
    }
}
