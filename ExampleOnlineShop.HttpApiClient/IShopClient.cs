using ExampleOnlineShop.Models;
using ExampleShop.Entities;

namespace HttpApiClient;

public interface IShopClient
{
    Task<IReadOnlyCollection<Product>> GetProducts();
    Task AddProduct(Product product);
    Task<Product> GetProduct(long id);
    Task UpdateProduct(long id, Product product);
    Task DeleteProduct(long id);
}