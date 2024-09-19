using Chocolatier.Data.Context;
using Chocolatier.Domain.Entities;
using Chocolatier.Domain.Interfaces.Repositories;

namespace Chocolatier.Data.Repositories
{
    public class OrderRepository : BaseRepository<Order>, IOrderRepository
    {
        public OrderRepository(ChocolatierContext chocolatierContext) : base(chocolatierContext)
        {
        }
    }
}
