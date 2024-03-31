using ExampleOnlineShop.Data.Genericrepository;
using ExampleOnlineShop.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ExampleOnlineShop.WebApi;

internal class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        var dbPath = "myapp.db";
        builder.Services.AddDbContext<AppDbContext>(void (options) =>
            options.UseSqlite($"Data Source={dbPath}"));
        //builder.Services.AddScoped<IProductRepository, ProductRepository>();
        builder.Services.AddScoped(typeof(IRepository<Product>), typeof(EfRepository<Product>));


        builder.Services.AddCors();


        var app = builder.Build();
        
        app.MapControllers();

        app.UseCors(policy =>
        {
            policy
                .WithOrigins("https://localhost:7251", "https://mysite.ru")
                .AllowAnyHeader()
                .AllowAnyMethod()
                ;
        });


        // app.MapGet("/products",
        //     async Task<IReadOnlyCollection<Product>> (
        //             [FromServices] IProductRepository repo,
        //             CancellationToken cancellationToken)
        //         => await repo.GetAll(cancellationToken));


        // app.MapGet($"/products/get_product", async (
        //         [FromServices] IProductRepository repo,
        //         [FromQuery] Guid id,
        //         CancellationToken cancellationToken)
        //     =>
        // {
        //     var product = await repo.GetById(id, cancellationToken);
        //     return product;
        // });


        // app.MapPost("/products/add_product", async (
        //     [FromServices] IProductRepository repo,
        //     [FromBody] Product product,
        //     CancellationToken cancellationToken) =>
        // {
        //     product.Id = Guid.NewGuid();
        //     await repo.Add(product, cancellationToken);
        // });


        // app.MapDelete("/del_product", async
        // (
        //     [FromQuery] Guid id,
        //     [FromServices] IProductRepository repo,
        //     CancellationToken cancellationToken
        // ) =>
        // {
        //     await repo.DeleteById(id, cancellationToken);
        // });


        // app.MapPut("/update_product_id", async (
        //     [FromServices] IProductRepository repo,
        //     [FromQuery] Guid id,
        //     [FromBody] Product updateProduct,
        //     CancellationToken cancellationToken) =>
        // {
        //     await repo.UpdateId(updateProduct, id, cancellationToken);
        // });


        // app.MapPut("/update_product", async (
        //     [FromServices] IProductRepository repo,
        //     [FromBody] Product updateProduct,
        //     CancellationToken cancellationToken) =>
        // {
        //     await repo.Update(updateProduct, cancellationToken);
        // });


        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();


        app.Run();
    }
}