using Chocolatier.Domain.Command.Order;
using Chocolatier.Domain.Entities;
using Chocolatier.Domain.Enum;
using Chocolatier.Domain.Events;
using Chocolatier.Domain.Interfaces;
using Chocolatier.Domain.Interfaces.Repositories;
using Chocolatier.Domain.Interfaces.Senders;
using Chocolatier.Domain.Responses;
using MediatR;
using System.Net;

namespace Chocolatier.Application.Handlers.OrdersHandlers
{
    public class ChangeOrderStatusHandler : IRequestHandler<ChangeOrderStatusCommand, Response>
    {
        private readonly IOrderRepository OrderRepository;
        private readonly IOrderHistoryRepository OrderHistoryRepository;
        private readonly IEmailQueueSender EmailQueueSender;
        private readonly IEstablishmentRepository EstablishmentRepository;
        private readonly IAuthEstablishment AuthEstablishment;

        public ChangeOrderStatusHandler(IOrderRepository orderRepository, IOrderHistoryRepository orderHistoryRepository, IEmailQueueSender emailQueueSender, IEstablishmentRepository establishmentRepository, IAuthEstablishment authEstablishment)
        {
            OrderRepository = orderRepository;
            OrderHistoryRepository = orderHistoryRepository;
            EmailQueueSender = emailQueueSender;
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

            emailsToSendNotify.Add(AuthEstablishment.Email);

            EmailQueueSender.SendEmailMessageQueue(new SendEmailEvent
            {
                Emails = emailsToSendNotify!,
                EmailTemplate = EmailTemplate.OrderUpdated
            });

            return new Response(true, HttpStatusCode.Created);
        }

        private bool VerifyStatusCanBeChanged(Order order, OrderStatus newOrderStatus)
        {
            return newOrderStatus switch
            {
                OrderStatus.Confirmed => order.CurrentStatus == OrderStatus.Pending,
                OrderStatus.OnPrepare => order.CurrentStatus == OrderStatus.Confirmed,
                OrderStatus.OnDelivery => order.CurrentStatus == OrderStatus.OnPrepare,
                OrderStatus.Done => order.CurrentStatus == OrderStatus.OnDelivery,
                _ => false
            };
        }
    }
}
