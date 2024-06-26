namespace ExampleOnlineShop.Domain.InterfacesRepositories
{
    public interface IRepository<TEntity>
    {
        Task<IReadOnlyCollection<TEntity>> GetAll(CancellationToken cancellationToken);
        Task<TEntity> GetById(Guid id,CancellationToken cancellationToken);
        Task Add(TEntity entity,CancellationToken cancellationToken);
        Task DeleteById(Guid id, CancellationToken cancellationToken);
        Task Update(TEntity entity, CancellationToken cancellationToken);
    }
}