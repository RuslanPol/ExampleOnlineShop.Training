using ExampleOnlineShop.Data.Repositories;
using ExampleOnlineShop.Domain;
using Microsoft.AspNetCore.Identity.Data;
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
    public async Task<ActionResult<Account>> GetByEmail(string email, CancellationToken cancellationToken)
    {
        if (email == null) throw new ArgumentNullException(nameof(email));
        var _account = await _accountRepository.GetByEmail(email, cancellationToken);
        return _account;
        
    }

    [HttpPost("register")]
    public async Task<ActionResult<Account>> Register(RegisterRequest request, CancellationToken cancellationToken=default)
    {
        if (request == null) throw new ArgumentNullException(nameof(request));
        var existingAccount = await _accountRepository.GetByEmail(request.Email, cancellationToken);
        if (existingAccount != null)
        {
            // Аккаунт уже существует, возвращаем сообщение об ошибке
            return BadRequest("An account with the given email already exists.");
        }
        var account = new Account(Guid.NewGuid(), request.Email,request.Email, request.Password);
        var accounts = await _accountRepository.GetAll(cancellationToken);
       
        await _accountRepository.Add(account, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id = account.Id }, account);
        
    }

    [HttpPut("update_account")]
    public async Task<IActionResult>  UpdateAccount(Account account, CancellationToken cancellationToken)
    {
        await _accountRepository.Update(account, cancellationToken);
        return Ok();
    }

    [HttpDelete("delete_account")]
    public async Task DeleteById(Guid id, CancellationToken cancellationToken)
    {
        await _accountRepository.DeleteById(id, cancellationToken);
    }
    
}