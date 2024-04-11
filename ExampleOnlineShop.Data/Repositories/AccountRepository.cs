using ExampleOnlineShop.Data.Genericrepository;
using ExampleOnlineShop.Domain;
using ExampleOnlineShop.Domain.Entities;
using ExampleOnlineShop.Domain.InterfacesRepositories;
using ExampleOnlineShop.WebApi.Data;
using Microsoft.EntityFrameworkCore;

namespace ExampleOnlineShop.Data.Repositories;

public class AccountRepository : EfRepository<Account>,IAccountRepository
{
    private readonly AppDbContext _dbContext;

    public AccountRepository(AppDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }

    public async Task<Account> GetByEmail(string email,CancellationToken cancellationToken)
    {
        if (email == null) throw new ArgumentNullException(nameof(email));
        var _account = await _dbContext.Accounts.FirstOrDefaultAsync(a => a.Email == email, cancellationToken);
        return _account;
    }

    public  async Task<bool> IsExistedEmail(string email, CancellationToken cancellationToken)
    {
        if (email == null) throw new ArgumentNullException(nameof(email));
        return await _dbContext.Accounts.AnyAsync(it=>it.Email==email,cancellationToken);
       
    }
}