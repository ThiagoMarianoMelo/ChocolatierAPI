using Chocolatier.Application.Senders;
using Chocolatier.Domain.ConfigObjects.Queues;
using Chocolatier.Domain.Interfaces.Senders;

namespace Chocolatier.API.Configurations
{
    public static class QueuesConfiguration
    {
        public static void ConfigureQueues(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<EmailQueueConfiguration>(configuration.GetSection(nameof(EmailQueueConfiguration)));

            services.AddScoped<IEmailQueueSender, EmailQueueSender>();
        }
    }
}
