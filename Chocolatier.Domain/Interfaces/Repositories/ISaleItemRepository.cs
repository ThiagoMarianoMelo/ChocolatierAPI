using Chocolatier.Domain.Entities;

namespace Chocolatier.Domain.Interfaces.Repositories
{
    public interface ISaleItemRepository : IBaseRepository<SaleItem>
    {
        Task<List<SaleItem>> GetItensFromSales(Guid saleId, CancellationToken cancellationToken);
    }
}
