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
                        RecipeId = p.RecipeId
                    })
                    .OrderBy(it => it.ExpireAt);
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
