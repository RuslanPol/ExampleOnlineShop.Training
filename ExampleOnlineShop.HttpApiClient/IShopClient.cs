using ExampleOnlineShop.Domain;
using ExampleOnlineShop.Domain.Entities;
using ExampleOnlineShop.HttpModels.Requests;

namespace ExampleOnlineShop.HttpApiClient;

public interface IShopClient
{
    Task<IReadOnlyCollection<Product>> GetProducts(CancellationToken cancellationToken=default);
    Task AddProduct(Product product,CancellationToken cancellationToken=default);
    Task<Product?> GetProduct(Guid id,CancellationToken cancellationToken=default);
    Task UpdateProduct(Guid id, Product product,CancellationToken cancellationToken=default);
    Task DeleteProduct(Guid id,CancellationToken cancellationToken=default);
    Task<IReadOnlyCollection<Account>> GetAccounts(CancellationToken cancellationToken=default);
    Task RegisterUser(RegisterRequest request,CancellationToken cancellationToken);
}