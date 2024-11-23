using Chocolatier.Domain.Entities;

namespace Chocolatier.Domain.Interfaces.Repositories
{
    public interface ICustomerRepository : IBaseRepository<Customer>
    {
        Task<IEnumerable<Customer>> GetCustomers(CancellationToken cancellationToken);
        Task<List<Customer>> GetNewCustomerByIntervalBasedOnDay(DateTime startDate, DateTime endDate, CancellationToken cancellationToken);
        Task<int> GetCountOfNewCustomerThisWeek(CancellationToken cancellationToken);
    }
}
