using Chocolatier.Data.Context;
using Chocolatier.Domain.Entities;
using Chocolatier.Domain.Interfaces.Repositories;

namespace Chocolatier.Data.Repositories
{
    public class RecipeRepository : BaseRepository<Recipe>, IRecipeRepository
    {
        public RecipeRepository(ChocolatierContext chocolatierContext) : base(chocolatierContext)
        {
        }
    }
}
