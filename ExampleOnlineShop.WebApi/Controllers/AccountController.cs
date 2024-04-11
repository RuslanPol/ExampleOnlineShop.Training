using ExampleOnlineShop.Data.Repositories;
using ExampleOnlineShop.Domain;
using ExampleOnlineShop.Domain.Entities;
using ExampleOnlineShop.Domain.InterfacesRepositories;
using ExampleOnlineShop.Domain.Services;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace ExampleOnlineShop.WebApi.Controllers;
[ApiController]
[Route("accounts")]
public class AccountController:ControllerBase
{
    private readonly AccountService _accountService;
    public AccountController(AccountService accountService)
    {
        _accountService = accountService;
       
    }
    
    [HttpGet("get_all")]
    public async Task<IReadOnlyCollection<Account>> GetAll(CancellationToken cancellationToken)
    {
        IReadOnlyCollection<Account> accounts = await _accountService.GetAll(cancellationToken);
        return accounts;
    }
    
    // [HttpGet("get_account_by_id")]
    // public async Task<Account> GetById(Guid id, CancellationToken cancellationToken)
    // {
    //     var _account = await _accountRepository.GetById(id, cancellationToken);
    //     return _account;
    // }
    //
    // [HttpGet("get_account_by_email")]
    // public async Task<ActionResult<Account>> GetByEmail(string email, CancellationToken cancellationToken)
    // {
    //     if (email == null) throw new ArgumentNullException(nameof(email));
    //     var _account = await _accountRepository.GetByEmail(email, cancellationToken);
    //     return _account;
    //     
    // }

    [HttpPost("register")]
    public async Task<ActionResult<Account>> Register(RegisterRequest request, CancellationToken cancellationToken=default)
    {
        try
        {
            var account=await _accountService.Register( request.Email, request.Email, request.Password,cancellationToken);
            return account;
        }
        catch ( AccountRegisteredExeption)
        {

            return BadRequest("an account with this email has already been registered");
        }  

    }

    // [HttpPut("update_account")]
    // public async Task<IActionResult>  UpdateAccount(Account account, CancellationToken cancellationToken)
    // {
    //     await _accountRepository.Update(account, cancellationToken);
    //     return Ok();
    // }

    // [HttpDelete("delete_account")]
    // public async Task DeleteById(Guid id, CancellationToken cancellationToken)
    // {
    //     await _accountRepository.DeleteById(id, cancellationToken);
    // }
    
}