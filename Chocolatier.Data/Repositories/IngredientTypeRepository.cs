using Chocolatier.Data.Context;
using Chocolatier.Domain.Entities;
using Chocolatier.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Chocolatier.Data.Repositories
{
    public class IngredientTypeRepository : BaseRepository<IngredientType>, IIngredientTypeRepository
    {
        public IngredientTypeRepository(ChocolatierContext chocolatierContext) : base(chocolatierContext)
        {
        }

        public async Task<IEnumerable<IngredientType>> GetIngredientTypes(CancellationToken cancellationToken)
        {

            return await DbSet.AsNoTracking()
                    .Where(it => it.IsActive)
                    .Select(es => new IngredientType()
                    {
                        Id = es.Id,
                        Name = es.Name,
                        MeasurementeUnit = es.MeasurementeUnit,
                        IsActive = es.IsActive
                    })
                    .OrderBy(it => it.Name)
                    .ToListAsync(cancellationToken);
        }

        public async Task<bool> IsActiveById(Guid Id, CancellationToken cancellationToken) => await DbSet.AnyAsync(it => it.Id == Id && it.IsActive, cancellationToken);

        public async Task<bool> IsDuplicatedName(string IngredientTypeName, CancellationToken cancellationToken) => await DbSet.AnyAsync(it => it.Name == IngredientTypeName && it.IsActive, cancellationToken);

    }
}
