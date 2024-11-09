using Chocolatier.Data.Context;
using Chocolatier.Domain.Entities;
using Chocolatier.Domain.Interfaces;
using Chocolatier.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Chocolatier.Data.Repositories
{
    public class CashCloseRepository : BaseRepository<CashClose>, ICashCloseRepository
    {
        private readonly IAuthEstablishment AuthEstablishment;
        public CashCloseRepository(ChocolatierContext chocolatierContext, IAuthEstablishment authEstablishment) : base(chocolatierContext)
        {
            AuthEstablishment = authEstablishment;
        }

        public async Task<List<CashClose>> GetCashCloseByDataFilter(DateTime startDate, DateTime endDate, CancellationToken cancellationToken)
        {
            return await DbSet.Where(p => p.EstablishmentId == AuthEstablishment.Id && p.Date.Date >= startDate.Date && p.Date.Date <= endDate.Date)
                                        .AsNoTracking()
                                        .Select(c => new CashClose { Id = c.Id, Date = c.Date, Billing = c.Billing, SaleQuantity = c.SaleQuantity })
                                        .ToListAsync(cancellationToken);

        }
    }
}
