using Chocolatier.Data.Context;
using Chocolatier.Domain.Entities;
using Chocolatier.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Chocolatier.Data.Repositories
{
    public class IngredientRepository : BaseRepository<Ingredient>, IIngredientRepository
    {
        public IngredientRepository(ChocolatierContext chocolatierContext) : base(chocolatierContext)
        {
        }
        public IQueryable<Ingredient> GetQueryableIngredientByFilter(DateTime initialDate, DateTime finalDate, Guid ingredientTypeId)
        {
            var queryCondiction = BuildQueryIngredientTypeFilter(initialDate.ToUniversalTime(), finalDate.ToUniversalTime(), ingredientTypeId);

            return DbSet.AsNoTracking()
                    .Where(queryCondiction)
                    .Select(i => new Ingredient()
                    {
                        Id = i.Id,
                        ExpireAt = i.ExpireAt,
                        Amount = i.Amount,
                        IngredientType = i.IngredientType
                    })
                    .OrderBy(it => it.ExpireAt);
        }

        public async Task<int> GetDisponibleAmountIngredientsByIngredientType(Guid ingredientTypeId, CancellationToken cancellationToken)
        {

            return await DbSet.AsNoTracking()
                    .Where(i => i.ExpireAt >= DateTime.UtcNow && i.IngredientTypeId == ingredientTypeId)
                    .Select(i => new Ingredient()
                    {
                        Amount = i.Amount,
                    }).SumAsync(i => i.Amount, cancellationToken);
        }

        private Expression<Func<Ingredient, bool>> BuildQueryIngredientTypeFilter(DateTime initialDate, DateTime finalDate, Guid ingredientTypeId)
        {
            var utcMinValue = DateTime.MinValue.ToUniversalTime();

            return i => (initialDate == utcMinValue || i.ExpireAt >= initialDate)
                     && (finalDate == utcMinValue || i.ExpireAt <= finalDate)
                     && (ingredientTypeId == Guid.Empty || ingredientTypeId == i.IngredientTypeId);
        }
    }
}
