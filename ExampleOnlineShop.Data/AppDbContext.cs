using ExampleOnlineShop.Domain;
using ExampleOnlineShop.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ExampleOnlineShop.WebApi.Data;

public class AppDbContext:DbContext

{
    public DbSet<Product> Products => Set<Product>();
    public DbSet<Account> Accounts => Set<Account>();
    //public object Response { get; set; }


    public AppDbContext(DbContextOptions<AppDbContext>options):base(options)
    {
        
    }

}