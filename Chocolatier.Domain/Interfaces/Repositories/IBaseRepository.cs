namespace Chocolatier.Domain.Interfaces.Repositories
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        Task<TEntity> Create(TEntity entity, CancellationToken cancellationToken);
        Task<int> SaveChanges(CancellationToken cancellationToken);
        Task<TEntity?> GetEntityById(Guid Id, CancellationToken cancellationToken);
        TEntity UpdateEntity(TEntity entity, CancellationToken cancellationToken);
    }
}
