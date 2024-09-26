using Chocolatier.Domain.Entities;

namespace Chocolatier.Domain.Interfaces.Repositories
{
    public interface IOrderHistoryRepository : IBaseRepository<OrderHistory>
    {
        Task<List<OrderHistory>> GetHistoryFromOrder(Guid orderId, CancellationToken cancellationToken);
    }
}
