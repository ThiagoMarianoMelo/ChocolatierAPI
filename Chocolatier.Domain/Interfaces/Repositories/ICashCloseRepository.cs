using Chocolatier.Domain.Entities;

namespace Chocolatier.Domain.Interfaces.Repositories
{
    public interface ICashCloseRepository : IBaseRepository<CashClose>
    {
        Task<List<CashClose>> GetCashCloseByDataFilter(DateTime startDate, DateTime endDate, CancellationToken cancellationToken);
    }
}
