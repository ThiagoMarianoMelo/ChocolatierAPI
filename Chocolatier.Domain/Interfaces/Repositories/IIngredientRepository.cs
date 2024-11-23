using Chocolatier.Domain.Entities;

namespace Chocolatier.Domain.Interfaces.Repositories
{
    public interface IIngredientRepository : IBaseRepository<Ingredient>
    {
        IQueryable<Ingredient> GetQueryableIngredientByFilter(DateTime initialDate, DateTime finalDate, Guid ingredientTypeId);
        Task<int> GetDisponibleAmountIngredientsByIngredientType(Guid ingredientTypeId, CancellationToken cancellationToken);
        Task<List<Ingredient>> GetDisponibleIngredientsByIngredientType(Guid ingredientTypeId, CancellationToken cancellationToken);
        Task<int> GetExpiredIngredientsCount(CancellationToken cancellationToken);
    }
}
