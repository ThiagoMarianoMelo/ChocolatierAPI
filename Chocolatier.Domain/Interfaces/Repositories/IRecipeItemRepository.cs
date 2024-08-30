using Chocolatier.Domain.Entities;

namespace Chocolatier.Domain.Interfaces.Repositories
{
    public interface IRecipeItemRepository : IBaseRepository<RecipeItem>
    {
        Task<List<RecipeItem>> GetItensFromRecipe(Guid recipeId, CancellationToken cancellationToken);
    }
}
