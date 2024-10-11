using Chocolatier.Data.Context;
using Chocolatier.Domain.Entities;
using Chocolatier.Domain.Interfaces.Repositories;

namespace Chocolatier.Data.Repositories
{
    public class CustomerRepository : BaseRepository<Customer>, ICustomerRepository
    {
        public CustomerRepository(ChocolatierContext chocolatierContext) : base(chocolatierContext)
        {
        }
    }
}
