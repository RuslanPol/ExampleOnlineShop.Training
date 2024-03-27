using ExampleOnlineShop.Models;

namespace ExampleOnlineShop.WebApi.Data
{
    public interface IProductRepository
    {
        Task<IReadOnlyCollection<Product>> GetAll(CancellationToken cancellationToken);
        Task<Product> GetById(Guid id,CancellationToken cancellationToken);
        Task Add(Product product, CancellationToken cancellationToken);
        Task UpdateId(Product product,Guid id,CancellationToken cancellationToken);
        Task Update(Product product,CancellationToken cancellationToken);
        Task DeleteById(Guid id,CancellationToken cancellationToken);
   
    }
}