using Chocolatier.Data.Context;
using Chocolatier.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Chocolatier.Data.Repositories
{
    public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        private readonly ChocolatierContext ChocolatierContext;
        protected readonly DbSet<TEntity> DbSet;

        protected BaseRepository(ChocolatierContext chocolatierContext)
        {
            ChocolatierContext = chocolatierContext;
            DbSet = ChocolatierContext.Set<TEntity>();
        }

        public async Task<TEntity> Create(TEntity entity, CancellationToken cancellationToken) 
        {
            return (await DbSet.AddAsync(entity, cancellationToken)).Entity;
        }

        public async Task<int> SaveChanges(CancellationToken cancellationToken)
        {
            return await ChocolatierContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<TEntity?> GetEntityById(Guid Id, CancellationToken cancellationToken)
        {
            return await DbSet.FindAsync([Id], cancellationToken);
        }

        public TEntity UpdateEntity(TEntity entity, CancellationToken cancellationToken)
        {
            return DbSet.Update(entity).Entity;
        }
    }
}
