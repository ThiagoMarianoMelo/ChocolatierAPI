using Chocolatier.Domain.Interfaces;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace Chocolatier.API.Consumer
{
    public abstract class RabbitMqConsumer
    {
        private readonly IQueueConfiguration QueueConfiguration;
        private readonly IModel Connection;

        public RabbitMqConsumer(IQueueConfiguration queueConfiguration)
        {
            QueueConfiguration = queueConfiguration;

            var factory = new ConnectionFactory { Uri = new Uri(QueueConfiguration.ConnectionString) };

            Connection = factory.CreateConnection().CreateModel();

            Connection.QueueDeclare(queue: QueueConfiguration.Queue, durable: true, exclusive: false, autoDelete: false);
        }

        protected void StartConsumer<T>()
        {
            var consumer = new EventingBasicConsumer(Connection);
            consumer.Received += (model, eventArgs) => {
                var body = eventArgs.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                var data = JsonSerializer.Deserialize<T>(message);
                ActionAfterConsume(data);
            };
            Connection.BasicConsume(queue: QueueConfiguration.Queue, autoAck: true, consumer: consumer);
        }

        public abstract void CreateConsumer();
        public abstract void ActionAfterConsume(object? dataConsumed);

    }
}
