using Chocolatier.Data.Context;
using Chocolatier.Domain.Entities;
using Chocolatier.Domain.Interfaces.Repositories;

namespace Chocolatier.Data.Repositories
{
    public class IngredientTypeRepository : BaseRepository<IngredientType>, IIngredientTypeRepository
    {
        public IngredientTypeRepository(ChocolatierContext chocolatierContext) : base(chocolatierContext)
        {
        }
    }
}
