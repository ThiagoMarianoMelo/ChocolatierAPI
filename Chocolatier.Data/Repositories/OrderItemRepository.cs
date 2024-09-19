using Chocolatier.Data.Context;
using Chocolatier.Domain.Entities;
using Chocolatier.Domain.Interfaces.Repositories;

namespace Chocolatier.Data.Repositories
{
    public class OrderItemRepository : BaseRepository<OrderItem>, IOrderItemRepository
    {
        public OrderItemRepository(ChocolatierContext chocolatierContext) : base(chocolatierContext)
        {
        }
    }
}
