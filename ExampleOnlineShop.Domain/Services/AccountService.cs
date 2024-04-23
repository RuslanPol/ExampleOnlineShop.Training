using ExampleOnlineShop.Domain.Entities;
using ExampleOnlineShop.Domain.InterfacesRepositories;

namespace ExampleOnlineShop.Domain.Services;

public class AccountService
{
    private readonly IAccountRepository _accountRepository;
    private readonly IPasswordHasherService _passwordHasherService;

    public AccountService(IAccountRepository accountRepository,IPasswordHasherService passwordHasherService)
    {
        _accountRepository = accountRepository ?? throw new ArgumentNullException(nameof(accountRepository));
        _passwordHasherService = passwordHasherService;
    }
/// <summary>
/// register new account
/// </summary>
/// <param name="name"></param>
/// <param name="email"></param>
/// <param name="password"></param>
/// <param name="cancellationToken"></param>
/// <returns>account</returns>
/// <exception cref="ArgumentNullException"></exception>
/// <exception cref="AccountRegisteredExeption"></exception>
    public virtual async Task<Account> Register(
        string name, string email, string password, CancellationToken cancellationToken = default)
    {
        if (name == null) throw new ArgumentNullException(nameof(name));
        if (email == null) throw new ArgumentNullException(nameof(email));
        if (password == null) throw new ArgumentNullException(nameof(password));

        var isExist = await _accountRepository.IsExistedEmail(email, cancellationToken);
        if (isExist)
        {
            throw new AccountRegisteredExeption("an account with this email has already been registered");
        }

        var passwordHash = _passwordHasherService.HashPassword(password); 
                           
        var account = new Account(Guid.NewGuid(), name, email, passwordHash);
        await _accountRepository.Add(account, cancellationToken);

        return account;
    }

    public virtual async Task<Account> Login(
        string email, string password, CancellationToken cancellationToken = default)
    {
        if (email == null) throw new ArgumentNullException(nameof(email));
        if (password == null) throw new ArgumentNullException(nameof(password));
        var existAccount = await _accountRepository.GetByEmail(email, cancellationToken);
        if (existAccount == null) throw new EmailNotFindExeption(email);
        var result = _passwordHasherService.VerifyPassword(existAccount.PasswordHash, password);
        if (!result)
        {
            throw new IncorrectPasswordExeption();
        }
        return existAccount;
    }

    public virtual async Task<IReadOnlyCollection<Account>> GetAll(CancellationToken cancellationToken)
    {
        var accounts = await _accountRepository.GetAll(cancellationToken);
        return accounts;
    }
}

public class IncorrectPasswordExeption : Exception
{
}

public class EmailNotFindExeption : Exception
{
    public EmailNotFindExeption(string email)
    {
        
    }
}

public class AccountRegisteredExeption : Exception
{
    public AccountRegisteredExeption(
        string anAccountWithThisEmailHasAlreadyBeenRegistered = null) : base(
        anAccountWithThisEmailHasAlreadyBeenRegistered)
    {
    }
}