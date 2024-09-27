namespace Chocolatier.Domain.Interfaces
{
    public interface IQueueConfiguration
    {
        public string ConnectionString { get; set; }
        public string Queue { get; set; }
    }
}
