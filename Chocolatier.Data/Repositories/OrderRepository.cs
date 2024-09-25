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

        public IQueryable<Order> GetQueryableOrdersFilter(DateTime initialDateDeadLine, DateTime finalDateDeadLine, DateTime initialDateCreatedAt, DateTime finalDateCreatedAt)
        {
            var queryCondiction = BuildQueryIngredientTypeFilter(initialDateDeadLine.ToUniversalTime(), finalDateDeadLine.ToUniversalTime(), initialDateCreatedAt.ToUniversalTime(), finalDateCreatedAt.ToUniversalTime());

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
        private Expression<Func<Order, bool>> BuildQueryIngredientTypeFilter(DateTime initialDateDeadLine, DateTime finalDateDeadLine, DateTime initialDateCreatedAt, DateTime finalDateCreatedAt)
        {
            var utcMinValue = DateTime.MinValue.ToUniversalTime();

            return or => (or.RequestedById == AuthEstablishment.Id || AuthEstablishment.EstablishmentType == EstablishmentType.Factory)
                        && (initialDateDeadLine == utcMinValue || or.DeadLine >= initialDateDeadLine)
                        && (finalDateDeadLine == utcMinValue || or.DeadLine <= finalDateDeadLine)
                        && (initialDateCreatedAt == utcMinValue || or.CreatedAt >= initialDateCreatedAt)
                        && (finalDateCreatedAt == utcMinValue || or.CreatedAt <= finalDateCreatedAt);
        }
    }
}
