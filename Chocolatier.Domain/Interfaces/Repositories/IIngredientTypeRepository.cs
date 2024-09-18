using Chocolatier.Domain.Entities;

namespace Chocolatier.Domain.Interfaces.Repositories
{
    public interface IIngredientTypeRepository : IBaseRepository<IngredientType>
    {
        Task<IEnumerable<IngredientType>> GetIngredientTypes(CancellationToken cancellationToken);
        Task<bool> IsActiveById(Guid Id, CancellationToken cancellationToken);
        Task<bool> IsDuplicatedName(string IngredientTypeName, CancellationToken cancellationToken);
    }
}
