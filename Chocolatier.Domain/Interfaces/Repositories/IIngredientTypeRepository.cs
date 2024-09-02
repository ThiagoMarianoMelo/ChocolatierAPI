using Chocolatier.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Chocolatier.Domain.Interfaces.Repositories
{
    public interface IIngredientTypeRepository : IBaseRepository<IngredientType>
    {
        IQueryable<IngredientType> GetQueryableIngredientTypesByFilter(string name);

        Task<bool> IsActiveById(Guid Id, CancellationToken cancellationToken);
    }
}
