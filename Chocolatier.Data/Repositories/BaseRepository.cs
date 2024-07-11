using Microsoft.EntityFrameworkCore;

namespace Chocolatier.Data.Repositories
{
    public abstract class BaseRepository<TEntity> where TEntity : class
    {
        private readonly DbContext ChocolatierContext;
        private readonly DbSet<TEntity> DbSet;

        protected BaseRepository(DbContext chocolatierContext)
        {
            ChocolatierContext = chocolatierContext;
            DbSet = ChocolatierContext.Set<TEntity>();
        }

        protected async Task<TEntity> Create(TEntity entity, CancellationToken cancellationToken) 
        {
            return (await DbSet.AddAsync(entity, cancellationToken)).Entity;
        }
    }
}
