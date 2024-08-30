using Chocolatier.Data.Context;
using Chocolatier.Domain.Entities;
using Chocolatier.Domain.Interfaces.Repositories;

namespace Chocolatier.Data.Repositories
{
    public class RecipeItemRepository : BaseRepository<RecipeItem>, IRecipeItemRepository
    {
        public RecipeItemRepository(ChocolatierContext chocolatierContext) : base(chocolatierContext)
        {
        }
    }
}
