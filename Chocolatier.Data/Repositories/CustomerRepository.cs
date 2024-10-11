using Chocolatier.Data.Context;
using Chocolatier.Domain.Entities;
using Chocolatier.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Chocolatier.Data.Repositories
{
    public class CustomerRepository : BaseRepository<Customer>, ICustomerRepository
    {
        public CustomerRepository(ChocolatierContext chocolatierContext) : base(chocolatierContext)
        {
        }

        public async Task<IEnumerable<Customer>> GetCustomers(CancellationToken cancellationToken)
        {

            return await DbSet.AsNoTracking()
                    .Select(c => new Customer()
                    {
                        Id = c.Id,
                        Name = c.Name,
                        Email = c.Email,
                        Phone = c.Phone,
                        Address = c.Address
                    })
                    .OrderBy(it => it.Name)
                    .ToListAsync(cancellationToken);
        }
    }
}
