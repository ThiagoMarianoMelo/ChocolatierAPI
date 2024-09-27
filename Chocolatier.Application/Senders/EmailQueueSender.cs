using Chocolatier.Domain.ConfigObjects.Queues;
using Chocolatier.Domain.Interfaces.Senders;
using Microsoft.Extensions.Options;

namespace Chocolatier.Application.Senders
{
    public class EmailQueueSender : RabbitMqSender, IEmailQueueSender
    {
        public EmailQueueSender(IOptions<EmailQueueConfiguration> QueueConfiguration) : base(QueueConfiguration.Value)
        {
        }

        public void SendEmailMessageQueue(string EmailMessage)
        {
            SendMessage(EmailMessage);
        }
    }
}
