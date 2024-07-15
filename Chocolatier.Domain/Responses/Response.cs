using Flunt.Notifications;
using System.Net;
using System.Text.Json.Serialization;

namespace Chocolatier.Domain.Responses
{
    public class Response
    {

        public Response(bool sucess, HttpStatusCode httpStatusCode)
        {
            Sucess = sucess;
            HttpStatusCode = httpStatusCode;
        }

        public Response(bool sucess, object data, HttpStatusCode httpStatusCode)
        {
            Sucess = sucess;
            Data = data;
            HttpStatusCode = httpStatusCode;
        }

        public Response(bool sucess, List<string> messages, HttpStatusCode httpStatusCode)
        {
            Sucess = sucess;
            Messages = messages;
            HttpStatusCode = httpStatusCode;
        }

        public Response(bool sucess, IReadOnlyCollection<Notification> notifications)
        {
            Sucess = sucess;
            HttpStatusCode = HttpStatusCode.BadRequest;  
            AddMessages(notifications);
        }

        public Response(bool sucess, object data, IReadOnlyCollection<Notification> notifications)
        {
            Sucess = sucess;
            Data = data;
            HttpStatusCode = HttpStatusCode.BadRequest;
            AddMessages(notifications);
        }

        public bool Sucess {  get; set; }
        public List<string> Messages { get; set; } = [];
        public object? Data { get; set; }

        [JsonIgnore]
        public HttpStatusCode HttpStatusCode { get; set; }


        private void AddMessages(IReadOnlyCollection<Notification> notifications)
        {
            foreach (var notification in notifications) { Messages.Add(notification.Message); }
        }
    }
}
