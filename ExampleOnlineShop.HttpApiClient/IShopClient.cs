using ExampleOnlineShop.Models;

namespace ExampleOnlineShop.HttpApiClient;

public interface IShopClient
{
    Task<IReadOnlyCollection<Product>> GetProducts();
    Task AddProduct(Product product);
    Task<Product?> GetProduct(Guid id);
    Task UpdateProduct(Guid id, Product product);
    Task DeleteProduct(Guid id);
}