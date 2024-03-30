using ExampleOnlineShop.Models;
using ExampleOnlineShop.WebApi.Data;
using Microsoft.EntityFrameworkCore;

namespace ExampleOnlineShop.Data.Genericrepository;

public class EfRepository<TEntity> : IRepository<TEntity> where TEntity : class, IEntity
{
    private readonly AppDbContext _dbContext;

    public EfRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    private DbSet<TEntity> _entities => _dbContext.Set<TEntity>();

    public async Task<IReadOnlyCollection<TEntity>> GetAll(CancellationToken cancellationToken)
    {
        var _collection = await _entities.ToListAsync(cancellationToken);
        return _collection;
    }

    public virtual async Task<TEntity> GetById(Guid id, CancellationToken cancellationToken)
    {
        var _entity = await _entities.FirstAsync(e => e.Id == id, cancellationToken);
        return _entity;
    }

    public virtual async Task Add(TEntity entity, CancellationToken cancellationToken)
    {
        await _entities.AddAsync(entity, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public virtual async Task DeleteById(Guid id, CancellationToken cancellationToken)
    {
        var _entity = await _entities.FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
        if (_entity != null) _entities.Remove(_entity);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task Update(TEntity entity, CancellationToken cancellationToken)
    {
        if (entity == null) throw new ArgumentNullException(nameof(entity));
        _dbContext.Entry(entity).State = EntityState.Modified; //регистрируем новый product
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}