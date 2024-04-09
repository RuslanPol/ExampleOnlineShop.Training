using ExampleOnlineShop.Models;

namespace ExampleOnlineShop.Data.Genericrepository
{
    public interface IRepository<TEntity> where TEntity : IEntity
    {
        Task<IReadOnlyCollection<TEntity>> GetAll(CancellationToken cancellationToken);
        Task<TEntity> GetById(Guid id,CancellationToken cancellationToken);
        Task Add(TEntity entity,CancellationToken cancellationToken);
        Task DeleteById(Guid id, CancellationToken cancellationToken);
        Task Update(TEntity entity, CancellationToken cancellationToken);
    }
}