using Chocolatier.Domain.Entities;

namespace Chocolatier.Domain.Interfaces.Repositories
{
    public interface IOrderRepository : IBaseRepository<Order>
    {
        IQueryable<Order> GetQueryableOrdersFilter(DateTime initialDateDeadLine, DateTime finalDateDeadLine, DateTime initialDateCreatedAt, DateTime finalDateCreatedAt);
    }
}
