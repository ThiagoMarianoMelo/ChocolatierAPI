using Chocolatier.Data.Context;
using Chocolatier.Domain.Entities;
using Chocolatier.Domain.Interfaces.Repositories;

namespace Chocolatier.Data.Repositories
{
    public class SaleItemRepository : BaseRepository<SaleItem>, ISaleItemRepository
    {
        public SaleItemRepository(ChocolatierContext chocolatierContext) : base(chocolatierContext)
        {
        }
    }
}
