namespace Chocolatier.Domain.Interfaces.Senders
{
    public interface IEmailQueueSender
    {
        public void SendEmailMessageQueue(string email);
    }
}
