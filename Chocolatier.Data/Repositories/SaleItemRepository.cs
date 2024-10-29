using Chocolatier.Data.Context;
using Chocolatier.Domain.Entities;
using Chocolatier.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Chocolatier.Data.Repositories
{
    public class SaleItemRepository : BaseRepository<SaleItem>, ISaleItemRepository
    {
        public SaleItemRepository(ChocolatierContext chocolatierContext) : base(chocolatierContext)
        {
        }

        public async Task<List<SaleItem>> GetItensFromSales(Guid saleId, CancellationToken cancellationToken)
        {
            return await DbSet.AsNoTracking()
                    .Where(si => si.SaleId == saleId)
                    .Select(ri => new SaleItem()
                    {
                        Id = ri.Id,
                        Quantity = ri.Quantity,
                        RecipeId = ri.RecipeId,
                        UnityPrice = ri.UnityPrice,
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
