using Chocolatier.Domain.Entities;
using Chocolatier.Domain.Enum;

namespace Chocolatier.Domain.Interfaces.Repositories
{
    public interface IOrderRepository : IBaseRepository<Order>
    {
        IQueryable<Order> GetQueryableOrdersFilter(OrderStatus? orderStatus, DateTime initialDateDeadLine, DateTime finalDateDeadLine, DateTime initialDateCreatedAt, DateTime finalDateCreatedAt);
        Task<string?> GetEstablishmentRequestFromOrder(Guid orderId, CancellationToken cancellationToken);
    }
}
