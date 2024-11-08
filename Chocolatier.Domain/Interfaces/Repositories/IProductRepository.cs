using Chocolatier.Domain.Entities;

namespace Chocolatier.Domain.Interfaces.Repositories
{
    public interface IProductRepository : IBaseRepository<Product>
    {
        IQueryable<Product> GetQueryableProductsByFilter(DateTime initialDate, DateTime finalDate, string productName);
        double GetProductPriceByRecipeId(Guid recipeid);
        Product DeleteProductById(Guid Id);
        Task<List<Product>> GetProductsOnStorageByRecipeId(Guid recipeId, CancellationToken cancellationToken);
        int GetProductQuantityInStorageByRecipeId(Guid recipeId);

        Task<List<Product>> GetExpiringProductsBasedOnDateFilter(DateTime startDate, DateTime endDate, CancellationToken cancellationToken);
    }
}
