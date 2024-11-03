using Chocolatier.Data.Context;
using Chocolatier.Domain.Entities;
using Chocolatier.Domain.Interfaces.Repositories;

namespace Chocolatier.Data.Repositories
{
    public class CashCloseRepository : BaseRepository<CashClose>, ICashCloseRepository
    {
        public CashCloseRepository(ChocolatierContext chocolatierContext) : base(chocolatierContext)
        {
        }
    }
}
