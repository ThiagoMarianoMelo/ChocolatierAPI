using Chocolatier.Data.Context;
using Chocolatier.Domain.Entities;
using Chocolatier.Domain.Enum;
using Chocolatier.Domain.Interfaces;
using Chocolatier.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Chocolatier.Data.Repositories
{
    public class SaleRepository : BaseRepository<Sale>, ISaleRepository
    {
        private readonly IAuthEstablishment AuthEstablishment;
        public SaleRepository(ChocolatierContext chocolatierContext, IAuthEstablishment authEstablishment) : base(chocolatierContext)
        {
            AuthEstablishment = authEstablishment;
        }

        public IQueryable<Sale> GetQueryableSalesByFilter(PaymentMethod? paymentMethod, Guid saleId, DateTime initialDateCreatedAt, DateTime finalDateCreatedAt)
        {
            var queryCondiction = BuildQuerySaleFilter(paymentMethod, saleId, initialDateCreatedAt.ToUniversalTime(), finalDateCreatedAt.ToUniversalTime());

            return DbSet.AsNoTracking()
                    .Where(queryCondiction)
                    .Select(s => new Sale()
                    {
                        Id = s.Id,
                        TotalPrice = s.TotalPrice,
                        PaymentMethod = s.PaymentMethod,
                        CustomerId = s.CustomerId,
                        SaleDate = s.SaleDate
                    })
                    .OrderByDescending(s => s.SaleDate);
        }

        public async Task<List<Sale>> GetSalesFromEstablishmentFromDay(string EstablishmentId, DateTime dayFilter, CancellationToken cancellationToken)
        {
            return await DbSet.AsNoTracking().Where(s => s.EstablishmentId == EstablishmentId && s.SaleDate > dayFilter && s.SaleDate < dayFilter.AddDays(1)).ToListAsync(cancellationToken);
        }

        public async Task<int> GetTotalSalesFromToday(CancellationToken cancellationToken)
        {
            return await DbSet.AsNoTracking().Where(s => s.EstablishmentId == AuthEstablishment.Id && s.SaleDate.Date == DateTime.UtcNow.Date).CountAsync(cancellationToken);
        }
        public async Task<double> GetTotalBillingFromToday(CancellationToken cancellationToken)
        {
            return await DbSet.AsNoTracking().Where(s => s.EstablishmentId == AuthEstablishment.Id && s.SaleDate.Date == DateTime.UtcNow.Date).SumAsync(s => s.TotalPrice, cancellationToken);
        }

        private Expression<Func<Sale, bool>> BuildQuerySaleFilter(PaymentMethod? paymentMethod, Guid saleId, DateTime initialDateCreatedAt, DateTime finalDateCreatedAt)
        {
            var utcMinValue = DateTime.MinValue.ToUniversalTime();

            return s => s.EstablishmentId == AuthEstablishment.Id
                        && (paymentMethod == null || s.PaymentMethod == paymentMethod)
                        && (saleId == Guid.Empty || s.Id == saleId)
                        && (initialDateCreatedAt == utcMinValue || s.SaleDate >= initialDateCreatedAt)
                        && (finalDateCreatedAt == utcMinValue || s.SaleDate <= finalDateCreatedAt);
        }
    }
}
