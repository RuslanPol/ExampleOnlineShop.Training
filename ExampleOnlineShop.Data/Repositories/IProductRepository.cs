using ExampleOnlineShop.Data.Genericrepository;
using ExampleOnlineShop.Models;

namespace ExampleOnlineShop.Data.Repositories
{
    public interface IProductRepository:IRepository<Product>
    {
        Task<Product> GetByName(string name, CancellationToken cancellationToken);

    }
}