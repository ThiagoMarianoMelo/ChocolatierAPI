using Chocolatier.Data.Context;
using Chocolatier.Domain.Entities;
using Chocolatier.Domain.Interfaces.Repositories;

namespace Chocolatier.Data.Repositories
{
    public class OrderHistoryRepository : BaseRepository<OrderHistory>, IOrderHistoryRepository
    {
        public OrderHistoryRepository(ChocolatierContext chocolatierContext) : base(chocolatierContext)
        {
        }
    }
}
