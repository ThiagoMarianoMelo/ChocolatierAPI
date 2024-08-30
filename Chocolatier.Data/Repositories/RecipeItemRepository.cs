using Chocolatier.Data.Context;
using Chocolatier.Domain.Entities;
using Chocolatier.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Chocolatier.Data.Repositories
{
    public class RecipeItemRepository : BaseRepository<RecipeItem>, IRecipeItemRepository
    {
        public RecipeItemRepository(ChocolatierContext chocolatierContext) : base(chocolatierContext)
        {
        }

        public async Task<List<RecipeItem>> GetItensFromRecipe(Guid recipeId, CancellationToken cancellationToken)
        {
            return await DbSet.AsNoTracking()
                    .Where(ri => ri.RecipeId == recipeId)
                    .Select(ri => new RecipeItem()
                    {
                        Id = ri.Id,
                        Quantity = ri.Quantity,
                        IngredientType = new IngredientType()
                        {
                            Name = ri.IngredientType.Name,
                            MeasurementeUnit = ri.IngredientType.MeasurementeUnit
                        }
                    })
                    .OrderBy(ri => ri.IngredientType!.Name)
                    .ToListAsync(cancellationToken);
        }

        public void DeleteItensFromRecipe(Guid recipeId)
        {
            DbSet.RemoveRange(DbSet.Where(ri => ri.RecipeId == recipeId));
        }
    }
}
