using Chocolatier.Domain.Events;

namespace Chocolatier.Domain.Interfaces.Senders
{
    public interface IEmailQueueSender
    {
        public void SendEmailMessageQueue(SendEmailEvent data);
    }
}
