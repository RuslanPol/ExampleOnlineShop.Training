using ExampleOnlineShop.Domain.Entities;

namespace ExampleOnlineShop.Domain.InterfacesRepositories
{
    public interface IProductRepository:IRepository<Product>
    {
        Task<Product> GetByName(string name, CancellationToken cancellationToken);
        

    }
}