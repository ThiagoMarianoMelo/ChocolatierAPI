using Chocolatier.Domain.Entities;

namespace Chocolatier.Domain.Interfaces.Repositories
{
    public interface IProductRepository : IBaseRepository<Product>
    {
        IQueryable<Product> GetQueryableProductsByFilter(DateTime initialDate, DateTime finalDate, string productName);
    }
}
