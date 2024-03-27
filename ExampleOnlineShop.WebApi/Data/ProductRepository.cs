using ExampleOnlineShop.Models;
using Microsoft.EntityFrameworkCore;

namespace ExampleOnlineShop.WebApi.Data;

public class ProductRepository : IProductRepository
{
    private readonly AppDbContext _dbContext;

    public ProductRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IReadOnlyCollection<Product>> GetAll(CancellationToken cancellationToken)
    {
        var _products = await _dbContext.Products.ToListAsync(cancellationToken);
        return _products.AsReadOnly();
    }

    public async Task<Product> GetById(Guid id, CancellationToken cancellationToken)
    {
        var _product = await _dbContext.Products.FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
        if (_product == null) throw new ArgumentNullException(nameof(_product));
        return _product;
    }

    public async Task Add(Product product, CancellationToken cancellationToken)
    {
        if (product == null) throw new ArgumentNullException(nameof(product));
        await _dbContext.Products.AddAsync(product, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateId(Product product, Guid id, CancellationToken cancellationToken)
    {
        if (product == null) throw new ArgumentNullException(nameof(product));
        var _product = await _dbContext.Products.FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
        product.Id = _product.Id;
        _dbContext.Entry(_product).State = EntityState.Detached; //разрегистрируем старый _product
        _dbContext.Entry(product).State = EntityState.Modified; //регистрируем новый product
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task Update(Product product, CancellationToken cancellationToken)
    {
        if (product == null) throw new ArgumentNullException(nameof(product));
        _dbContext.Entry(product).State = EntityState.Modified; //регистрируем новый product
        await _dbContext.SaveChangesAsync(cancellationToken);
    }


    public async Task DeleteById(Guid id, CancellationToken cancellationToken)
    {
        var _product = await _dbContext.Products.FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
        _dbContext.Products.Remove(_product);
        await _dbContext.SaveChangesAsync();
    }
}