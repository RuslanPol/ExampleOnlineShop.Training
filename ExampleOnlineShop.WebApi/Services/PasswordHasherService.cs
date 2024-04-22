using ExampleOnlineShop.Domain.Entities;
using ExampleOnlineShop.Domain.Services;
using Microsoft.AspNetCore.Identity;

namespace ExampleOnlineShop.WebApi.Services;

public class PasswordHasherService : IPasswordHasherService
{
    private readonly Account _accountFake = new Account(Guid.NewGuid(), "Fake", "fake@fake", "");
    private readonly PasswordHasher<Account> _hasher ;

    
    public string HashPassword(string password)
    {
        if (password == null) throw new ArgumentNullException(nameof(password));
        
            var hashPassword = _hasher.HashPassword(_accountFake,password);
            return hashPassword;
    }

    public bool VerifyPassword(string passwordHash, string passwordVerify)
    {
        PasswordVerificationResult result = _hasher.VerifyHashedPassword(
            _accountFake,passwordHash, passwordVerify);
        return result != PasswordVerificationResult.Failed;

    }
}

