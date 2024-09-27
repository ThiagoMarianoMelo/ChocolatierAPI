using Chocolatier.Domain.Interfaces;

namespace Chocolatier.Domain.ConfigObjects.Queues
{
    public class EmailQueueConfiguration : IQueueConfiguration
    {
        public string ConnectionString { get; set; } = string.Empty;
        public string Queue { get; set; } = string.Empty;
    }
}
