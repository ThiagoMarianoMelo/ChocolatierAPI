using Chocolatier.Domain.Entities;

namespace Chocolatier.Domain.Interfaces.Repositories
{
    public interface IIngredientTypeRepository : IBaseRepository<IngredientType>
    {
        IQueryable<IngredientType> GetQueryableIngredientTypesByFilter(string name);
    }
}
