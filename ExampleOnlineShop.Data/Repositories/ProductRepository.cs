using ExampleOnlineShop.Data.Genericrepository;
using ExampleOnlineShop.Domain.Entities;
using ExampleOnlineShop.Domain.InterfacesRepositories;
using ExampleOnlineShop.WebApi.Data;
using Microsoft.EntityFrameworkCore;

namespace ExampleOnlineShop.Data.Repositories;

public class ProductRepository : EfRepository<Product>, IProductRepository
{
    private readonly AppDbContext _dbContext;

    public ProductRepository(AppDbContext dbContext):base(dbContext)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }
    public async Task<Product> GetByName(string name, CancellationToken cancellationToken)
    {
        var _product = await _dbContext.Products.FirstOrDefaultAsync(p => p.Name == name, cancellationToken);
        if (_product == null) throw new ArgumentNullException(nameof(_product));
        return _product;
    }

    
}