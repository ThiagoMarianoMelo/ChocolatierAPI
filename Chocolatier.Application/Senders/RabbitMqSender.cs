using Chocolatier.Domain.Interfaces;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace Chocolatier.Application.Senders
{
    public abstract class RabbitMqSender
    {
        private readonly IQueueConfiguration QueueConfiguration;
        private readonly IModel Connection;
        public RabbitMqSender(IQueueConfiguration queueConfiguration)
        {
            QueueConfiguration = queueConfiguration;

            var factory = new ConnectionFactory { Uri = new Uri(QueueConfiguration.ConnectionString) };

            Connection = factory.CreateConnection().CreateModel();

            Connection.QueueDeclare(queue: QueueConfiguration.Queue, durable: true, exclusive: false, autoDelete: false);
        }

        protected void SendMessage(Object message)
        {
            var dataString = JsonSerializer.Serialize(message);
            var body = Encoding.UTF8.GetBytes(dataString);

            Connection.BasicPublish(exchange: "",
                                 routingKey: QueueConfiguration.Queue,
                                 basicProperties: null,
                                 body: body);

            Connection.Close();
        }
    }
}
