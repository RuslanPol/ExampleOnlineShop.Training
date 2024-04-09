using ExampleOnlineShop.Data.Repositories;
using ExampleOnlineShop.Models;
using Microsoft.AspNetCore.Mvc;

namespace ExampleOnlineShop.WebApi.Controllers;
[ApiController]
[Route("accounts")]
public class AccountController:ControllerBase
{
    private readonly IAccountRepository _accountRepository;

    public AccountController(IAccountRepository accountRepository)
    {
        _accountRepository = accountRepository;
    }

    [HttpGet("get_all")]
    public async Task<IReadOnlyCollection<Account>> GetAll(CancellationToken cancellationToken)
    {
        var _accounts = await _accountRepository.GetAll(cancellationToken);
        return _accounts;
    }

    [HttpGet("get_account_by_id")]
    public async Task<Account> GetById(Guid id, CancellationToken cancellationToken)
    {
        var _account = await _accountRepository.GetById(id, cancellationToken);
        return _account;
    }

    [HttpGet("get_account_by_email")]
    public async Task<Account> GetByEmail(string email, CancellationToken cancellationToken)
    {
        var _account = await _accountRepository.GetByEmail(email, cancellationToken);
        return _account;
        
    }

    [HttpPost("add_account")]
    public async Task Add([FromBody]Account account, CancellationToken cancellationToken)
    {
        await _accountRepository.Add(account, cancellationToken);
    }

    [HttpPut("update_account")]
    public async Task UpdateAccount(Account account, CancellationToken cancellationToken)
    {
        await _accountRepository.Update(account, cancellationToken);
    }

    [HttpDelete("delete_account")]
    public async Task DeleteById(Guid id, CancellationToken cancellationToken)
    {
        await _accountRepository.DeleteById(id, cancellationToken);
    }
    
}