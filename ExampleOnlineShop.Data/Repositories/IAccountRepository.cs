using ExampleOnlineShop.Data.Genericrepository;
using ExampleOnlineShop.Domain;
using ExampleOnlineShop.Models;

namespace ExampleOnlineShop.Data.Repositories;

public interface IAccountRepository:IRepository<Account>
{
    Task<Account> GetByEmail(string email,CancellationToken cancellationToken);
}