using Chocolatier.Data.Context;
using Chocolatier.Domain.Entities;
using Chocolatier.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Chocolatier.Data.Repositories
{
    public class OrderHistoryRepository : BaseRepository<OrderHistory>, IOrderHistoryRepository
    {
        public OrderHistoryRepository(ChocolatierContext chocolatierContext) : base(chocolatierContext)
        {
        }

        public async Task<List<OrderHistory>> GetHistoryFromOrder(Guid orderId, CancellationToken cancellationToken)
        {
            return await DbSet.AsNoTracking()
                    .Where(oh => oh.OrderId == orderId)
                    .Select(oh => new OrderHistory()
                    {
                        NewStatus = oh.NewStatus,
                        ChangedAt = oh.ChangedAt
                    })
                    .OrderBy(oh => oh.ChangedAt)
                    .ToListAsync(cancellationToken);
        }
    }
}
