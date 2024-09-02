using Chocolatier.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Chocolatier.Domain.Interfaces.Repositories
{
    public interface IRecipeRepository : IBaseRepository<Recipe>
    {
        IQueryable<Recipe> GetQueryableRecipesFilter(string name);
        Task<bool> IsActiveById(Guid Id, CancellationToken cancellationToken);
    }
}
