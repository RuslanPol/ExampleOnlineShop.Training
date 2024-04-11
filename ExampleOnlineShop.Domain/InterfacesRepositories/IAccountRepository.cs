using ExampleOnlineShop.Domain.Entities;

namespace ExampleOnlineShop.Domain.InterfacesRepositories;

public interface IAccountRepository:IRepository<Account>
{
    Task<Account> GetByEmail(string email,CancellationToken cancellationToken);
    Task<bool> IsExistedEmail(string email, CancellationToken cancellationToken);
}