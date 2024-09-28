﻿using Chocolatier.Domain.Command.Order;
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
    public class CancelOrderHandler : IRequestHandler<CancelOrderCommand, Response>
    {
        private readonly IOrderRepository OrderRepository;
        private readonly IOrderHistoryRepository OrderHistoryRepository;
        private readonly IEmailQueueSender EmailQueueSender;
        private readonly IEstablishmentRepository EstablishmentRepository;
        private readonly IAuthEstablishment AuthEstablishment;

        public CancelOrderHandler(IOrderHistoryRepository orderHistoryRepository, IOrderRepository orderRepository, IEmailQueueSender emailQueueSender, IEstablishmentRepository establishmentRepository, IAuthEstablishment authEstablishment)
        {
            OrderHistoryRepository = orderHistoryRepository;
            OrderRepository = orderRepository;
            EmailQueueSender = emailQueueSender;
            EstablishmentRepository = establishmentRepository;
            AuthEstablishment = authEstablishment;
        }

        public async Task<Response> Handle(CancelOrderCommand request, CancellationToken cancellationToken)
        {
            request.Validate();
            if (!request.IsValid)
                return new Response(false, request.Notifications);

            var order = await OrderRepository.GetEntityById(request.Id, cancellationToken);

            if (order is null)
                return new Response(true, ["Erro ao encontrar pedido."], HttpStatusCode.InternalServerError);

            if (order.CurrentStatus == OrderStatus.Canceled)
                return new Response(true, ["O pedido já está cancelado."], HttpStatusCode.InternalServerError);

            var orderHistoryResult = await OrderHistoryRepository.Create(new OrderHistory
            {
                OrderId = order.Id,
                NewStatus = OrderStatus.Canceled,
                ChangedAt = DateTime.UtcNow
            }, cancellationToken);

            if (orderHistoryResult is null)
                return new Response(true, ["Erro ao cancelar pedido."], HttpStatusCode.InternalServerError);

            order.CurrentStatus = OrderStatus.Canceled;
            order.CancelReason = request.CancelReason;

            var resultUpdateOrder = OrderRepository.UpdateEntity(order, cancellationToken);

            if (resultUpdateOrder is null)
                return new Response(true, ["Erro ao cancelar pedido."], HttpStatusCode.InternalServerError);

            await OrderRepository.SaveChanges(cancellationToken);

            var emailsToSendNotify = await EstablishmentRepository.GetFactoryEmails(cancellationToken);

            emailsToSendNotify.Add(AuthEstablishment.Email);

            EmailQueueSender.SendEmailMessageQueue(new SendEmailEvent
            {
                Emails = emailsToSendNotify!,
                EmailTemplate = EmailTemplate.OrderCanceled
            });

            return new Response(true, HttpStatusCode.Created);
        }
    }
}
