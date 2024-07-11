using Chocolatier.Domain.Responses;
using Flunt.Notifications;
using MediatR;

namespace Chocolatier.Domain.Command
{
    public abstract class BaseComamnd : Notifiable<Notification>, IRequest<Response>
    {
        public List<string> GetMessages()
        {
            return Notifications.Select(ntf => ntf.Message).ToList();
        }

    }
}
