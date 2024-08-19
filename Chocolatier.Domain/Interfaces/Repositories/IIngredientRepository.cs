using Chocolatier.Domain.Entities;

namespace Chocolatier.Domain.Interfaces.Repositories
{
    public interface IIngredientRepository : IBaseRepository<Ingredient>
    {
        IQueryable<Ingredient> GetQueryableIngredientByFilter(DateTime initialDate, DateTime finalDate, Guid ingredientTypeId);
    }
}
