using Chocolatier.Domain.Enum;
using Chocolatier.Domain.Events;
using Chocolatier.Domain.Interfaces.Senders;

namespace Chocolatier.Application.Handlers.OrdersHandlers
{
    public abstract class BaseOrderHandler
    {
        private readonly IEmailQueueSender EmailQueueSender;

        protected BaseOrderHandler(IEmailQueueSender emailQueueSender)
        {
            EmailQueueSender = emailQueueSender;
        }
        protected async Task SendEmailOrder(List<string> emailsToNotify, EmailTemplate emailTemplate, Dictionary<string, string> emailParams)
        {

            EmailQueueSender.SendEmailMessageQueue(new SendEmailEvent
            {
                Emails = emailsToNotify,
                EmailTemplate = emailTemplate,
                Params = emailParams
            });

            await Task.CompletedTask;
        }
    }
}
