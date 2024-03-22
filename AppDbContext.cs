using ExampleShop.Entities;
using Microsoft.EntityFrameworkCore;

namespace ExampleShop;

public class AppDbContext:DbContext

{
    public DbSet<Product> Products => Set<Product>();
    //public object Response { get; set; }


    public AppDbContext(DbContextOptions<AppDbContext>options):base(options)
    {
        
    }

}