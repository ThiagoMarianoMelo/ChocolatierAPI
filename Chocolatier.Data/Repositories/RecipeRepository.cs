using Chocolatier.Data.Context;
using Chocolatier.Domain.Entities;
using Chocolatier.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Chocolatier.Data.Repositories
{
    public class RecipeRepository : BaseRepository<Recipe>, IRecipeRepository
    {
        public RecipeRepository(ChocolatierContext chocolatierContext) : base(chocolatierContext)
        {
        }

        public IQueryable<Recipe> GetQueryableRecipesFilter(string name)
        {
            var queryCondiction = BuildQueryIngredientTypeFilter(name);

            return DbSet.AsNoTracking()
                    .Where(queryCondiction)
                    .Select(es => new Recipe()
                    {
                        Id = es.Id,
                        Name = es.Name
                    })
                    .OrderBy(it => it.Name);
        }

        private Expression<Func<Recipe, bool>> BuildQueryIngredientTypeFilter(string name)
        {
            return r => (string.IsNullOrWhiteSpace(name) || r.Name!.Contains(name));
        }
    }
}
