using Chocolatier.Domain.Entities;

namespace Chocolatier.Domain.Interfaces.Repositories
{
    public interface IOrderItemRepository : IBaseRepository<OrderItem>
    {
        Task<List<OrderItem>> GetItensFromOrder(Guid orderId, CancellationToken cancellationToken);
    }
}
