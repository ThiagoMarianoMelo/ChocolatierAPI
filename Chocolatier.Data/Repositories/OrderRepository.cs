using Chocolatier.Data.Context;
using Chocolatier.Domain.Entities;
using Chocolatier.Domain.Enum;
using Chocolatier.Domain.Interfaces;
using Chocolatier.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Chocolatier.Data.Repositories
{
    public class OrderRepository : BaseRepository<Order>, IOrderRepository
    {
        private readonly IAuthEstablishment AuthEstablishment;

        public OrderRepository(ChocolatierContext chocolatierContext, IAuthEstablishment authEstablishment) : base(chocolatierContext)
        {
            AuthEstablishment = authEstablishment;
        }

        public IQueryable<Order> GetQueryableOrdersFilter(OrderStatus? orderStatus, DateTime initialDateDeadLine, DateTime finalDateDeadLine, 
            DateTime initialDateCreatedAt, DateTime finalDateCreatedAt)
        {
            var queryCondiction = BuildQueryIngredientTypeFilter(orderStatus, initialDateDeadLine.ToUniversalTime(), finalDateDeadLine.ToUniversalTime(), 
                initialDateCreatedAt.ToUniversalTime(), finalDateCreatedAt.ToUniversalTime());

            return DbSet.AsNoTracking()
                    .Where(queryCondiction)
                    .Select(or => new Order()
                    {
                        Id = or.Id,
                        CurrentStatus = or.CurrentStatus,
                        CreatedAt = or.CreatedAt,
                        DeadLine = or.DeadLine
                    })
                    .OrderBy(or => or.CreatedAt);
        }


        public async Task<string?> GetEstablishmentRequestFromOrder(Guid orderId, CancellationToken cancellationToken)
        {
             return await DbSet.AsNoTracking()
                        .Where(or => or.Id == orderId)
                        .Select(or => or.RequestedById)
                        .FirstOrDefaultAsync(cancellationToken);
        }
        private Expression<Func<Order, bool>> BuildQueryIngredientTypeFilter(OrderStatus? orderStatus, DateTime initialDateDeadLine, DateTime finalDateDeadLine, 
            DateTime initialDateCreatedAt, DateTime finalDateCreatedAt)
        {
            var utcMinValue = DateTime.MinValue.ToUniversalTime();

            return or => (or.RequestedById == AuthEstablishment.Id || AuthEstablishment.EstablishmentType == EstablishmentType.Factory)
                        && (orderStatus == null || or.CurrentStatus == orderStatus)
                        && (initialDateDeadLine == utcMinValue || or.DeadLine >= initialDateDeadLine)
                        && (finalDateDeadLine == utcMinValue || or.DeadLine <= finalDateDeadLine)
                        && (initialDateCreatedAt == utcMinValue || or.CreatedAt >= initialDateCreatedAt)
                        && (finalDateCreatedAt == utcMinValue || or.CreatedAt <= finalDateCreatedAt);
        }
    }
}
