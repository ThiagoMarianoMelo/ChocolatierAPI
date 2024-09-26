using Chocolatier.Data.Context;
using Chocolatier.Domain.Entities;
using Chocolatier.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Chocolatier.Data.Repositories
{
    public class OrderItemRepository : BaseRepository<OrderItem>, IOrderItemRepository
    {
        public OrderItemRepository(ChocolatierContext chocolatierContext) : base(chocolatierContext)
        {
        }

        public async Task<List<OrderItem>> GetItensFromOrder(Guid orderId, CancellationToken cancellationToken)
        {
            return await DbSet.AsNoTracking()
                    .Where(ri => ri.OrderId == orderId)
                    .Select(ri => new OrderItem()
                    {
                        Id = ri.Id,
                        Quantity = ri.Quantity,
                        RecipeId = ri.RecipeId,
                        Recipe = new Recipe()
                        {
                            Name = ri.Recipe!.Name
                        }

                    })
                    .OrderBy(ri => ri.Recipe!.Name)
                    .ToListAsync(cancellationToken);
        }
    }
}
