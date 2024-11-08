using Chocolatier.Data.Context;
using Chocolatier.Domain.Entities;
using Chocolatier.Domain.Interfaces;
using Chocolatier.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Chocolatier.Data.Repositories
{
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        private readonly IAuthEstablishment AuthEstablishment;

        public ProductRepository(ChocolatierContext chocolatierContext, IAuthEstablishment authEstablishment) : base(chocolatierContext)
        {
            AuthEstablishment = authEstablishment;
        }

        public IQueryable<Product> GetQueryableProductsByFilter(DateTime initialDate, DateTime finalDate, string productName)
        {
            var queryCondiction = BuildQueryProductsTypeFilter(initialDate.ToUniversalTime(), finalDate.ToUniversalTime(), productName);

            return DbSet.AsNoTracking()
                    .Where(queryCondiction)
                    .Select(p => new Product()
                    {
                        Id = p.Id,
                        Name = p.Name,
                        ExpireAt = p.ExpireAt,
                        Price = p.Price,
                        RecipeId = p.RecipeId,
                        Quantity = p.Quantity
                    })
                    .OrderBy(it => it.ExpireAt);
        }

        public Product DeleteProductById(Guid Id)
        {
            return DbSet.Remove(DbSet.Find(Id)!).Entity;
        }

        public async Task<List<Product>> GetProductsOnStorageByRecipeId(Guid recipeId, CancellationToken cancellationToken)
            => await DbSet.AsNoTracking()
            .Where(p => p.RecipeId == recipeId && p.ExpireAt > DateTime.UtcNow && p.CurrentEstablishmentId == AuthEstablishment.Id)
            .OrderBy(p => p.Quantity)
            .ToListAsync(cancellationToken);

        public int GetProductQuantityInStorageByRecipeId(Guid recipeId)
         => DbSet.AsNoTracking()
            .Where(p => p.RecipeId == recipeId && p.ExpireAt > DateTime.UtcNow && p.CurrentEstablishmentId == AuthEstablishment.Id).Sum(p => p.Quantity);


        public double GetProductPriceByRecipeId(Guid recipeid)
            => DbSet.AsNoTracking().FirstOrDefault(p => p.RecipeId == recipeid && p.ExpireAt > DateTime.UtcNow && p.CurrentEstablishmentId == AuthEstablishment.Id)?.Price ?? 0;


        public async Task<List<Product>> GetExpiringProductsBasedOnDateFilter(DateTime startDate, DateTime endDate, CancellationToken cancellationToken)
        {
            return await DbSet.Where(p => p.CurrentEstablishmentId == AuthEstablishment.Id && p.ExpireAt.Date >= startDate.Date && p.ExpireAt.Date <= endDate.Date)
                                        .AsNoTracking()
                                        .Select(c => new Product { Id = c.Id, ExpireAt = c.ExpireAt })
                                        .ToListAsync(cancellationToken);

        }

        private Expression<Func<Product, bool>> BuildQueryProductsTypeFilter(DateTime initialDate, DateTime finalDate, string productName)
        {
            var utcMinValue = DateTime.MinValue.ToUniversalTime();

            return p => AuthEstablishment.Id == p.CurrentEstablishmentId
                     && (initialDate == utcMinValue || p.ExpireAt >= initialDate)
                     && (finalDate == utcMinValue || p.ExpireAt <= finalDate)
                     && (string.IsNullOrWhiteSpace(productName) || p.Name.Contains(productName));
        }
    }
}
