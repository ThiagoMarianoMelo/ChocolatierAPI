using Chocolatier.API.Consumer;
using Chocolatier.Application.Senders;
using Chocolatier.Domain.ConfigObjects.Queues;
using Chocolatier.Domain.Interfaces.Senders;
using System.Reflection;

namespace Chocolatier.API.Configurations
{
    public static class QueuesConfiguration
    {

        public static void ConfigureQueues(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<EmailQueueConfiguration>(configuration.GetSection(nameof(EmailQueueConfiguration)));
            services.AddScoped<IEmailQueueSender, EmailQueueSender>();
            services.AddScoped<EmailQueueConsumer>();
        }

        public static void StartQueuesConsumer(IServiceScopeFactory scopeFactory)
        {

            var consumerTypes = Assembly.GetExecutingAssembly()
                                        .GetTypes()
                                        .Where(t => t.IsSubclassOf(typeof(RabbitMqConsumer)) && !t.IsAbstract);

            using var scope = scopeFactory.CreateScope();
            
            var serviceProvider = scope.ServiceProvider;

            foreach (var type in consumerTypes)
            {
                var instance = (RabbitMqConsumer)serviceProvider.GetRequiredService(type);

                instance?.CreateConsumer();
            }
            
        }
    }
}
