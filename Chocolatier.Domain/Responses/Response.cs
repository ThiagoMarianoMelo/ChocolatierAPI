using Flunt.Notifications;

namespace Chocolatier.Domain.Responses
{
    public class Response
    {

        public Response(bool sucess)
        {
            Sucess = sucess;
        }

        public Response(bool sucess, object data)
        {
            Sucess = sucess;
            Data = data;
        }

        public Response(bool sucess, List<string> messages)
        {
            Sucess = sucess;
            Messages = messages;
        }

        public Response(bool sucess, IReadOnlyCollection<Notification> notifications)
        {
            Sucess = sucess;
            AddMessages(notifications);
        }

        public Response(bool sucess, object data, IReadOnlyCollection<Notification> notifications)
        {
            Sucess = sucess;
            Data = data;
            AddMessages(notifications);
        }

        public bool Sucess {  get; set; }
        public List<string> Messages { get; set; } = [];
        public object? Data { get; set; }


        private void AddMessages(IReadOnlyCollection<Notification> notifications)
        {
            foreach (var notification in notifications) { Messages.Add(notification.Message); }
        }
    }
}
