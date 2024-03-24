using ExampleOnlineShop.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ExampleOnlineShop.WebApi;

internal class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        var dbPath = "myapp.db";
        builder.Services.AddDbContext<AppDbContext>(void (options) =>
            options.UseSqlite($"Data Source={dbPath}"));
        builder.Services.AddCors();


        var app = builder.Build();
        app.UseCors(policy =>
        {
            policy
                .WithOrigins("https://localhost:7251", "https://mysite.ru")
                .AllowAnyHeader()
                .AllowAnyMethod()
                ;
        });


        app.MapGet("/products", async Task<List<Product>> (AppDbContext context)
            => await context.Products.ToListAsync());

        app.MapGet($"/products/get_product", async (
                [FromServices]AppDbContext context,
                [FromQuery] Guid id)
            =>
        {
            var product = await context.Products.FirstOrDefaultAsync(p => p.Id == id);
            return product;
            // if (product is null)
            // {
            //     return Results.NotFound($"Product whith  id = {id} not find!");
            // }
            //
            // return Results.Ok($"{product.Name}");
        });

        app.MapPost("/products", async (
            [FromServices] AppDbContext context,
            [FromBody] Product product) =>
        {
            product.Id=Guid.NewGuid();
            await context.AddAsync(product);
            await context.SaveChangesAsync();
        });

        

        app.MapDelete("/del_product", async ([FromBody] Guid id,[FromServices] AppDbContext context) =>
        {
            Product? product = await context.Products.FirstOrDefaultAsync(p => p.Id == id);
            if (product != null)
            {
                context.Remove(product);
                await context.SaveChangesAsync();
            }
        });
        app.MapDelete("/delete_product", async ([FromQuery] Guid id, AppDbContext context) =>
        {
            Product? product = await context.Products.FirstOrDefaultAsync(p => p.Id == id);
            if (product != null)
            {
                context.Remove(product);
                await context.SaveChangesAsync();
            }
        });

        app.MapPut("/products/update_product",
            async ([FromQuery] Guid id, AppDbContext context, Product updateProduct) =>
            {
                var product = await context.Products.FirstOrDefaultAsync(p => p.Id == id);
                if (product != null)
                {
                    product.Name = updateProduct.Name;
                    product.Price = updateProduct.Price;
                    product.Stock = updateProduct.Stock;
                    product.Description = updateProduct.Description;
                    product.Image = updateProduct.Image;
                    await context.SaveChangesAsync();
                }
            });
        app.MapPut("/update_product", async (
            [FromQuery] Guid id,
            [FromServices] AppDbContext context,
            Product updateProduct) =>
        {
            var product = await context.Products.FirstOrDefaultAsync(p => p.Id == id);
            if (product is null)
            {
                return Results.NotFound($"Product whith  id = {id} not find!");
            }

            context.Entry(product).State = EntityState.Detached; //разрегистрируем product
            context.Entry(updateProduct).State = EntityState.Modified; //регистрируем  новый измененный
            await context.SaveChangesAsync();
            return Results.Ok("ok");
        });

        // app.MapPut("/products/{prodictId}", async Task (AppDbContext context, Product product) =>
        // {
        //
        //     context.Update(product);
        //     await context.SaveChangesAsync();
        //
        // });


// Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
//
// var summaries = new[]
// {
//     "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
// };
//
// app.MapGet("/weatherforecast", () =>
//     {
//         var forecast = Enumerable.Range(1, 5).Select(index =>
//                 new WeatherForecast
//                 (
//                     DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
//                     Random.Shared.Next(-20, 55),
//                     summaries[Random.Shared.Next(summaries.Length)]
//                 ))
//             .ToArray();
//         return forecast;
//     })
//     .WithName("GetWeatherForecast")
//     .WithOpenApi();

        app.Run();
    }
}

// record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
// {
//     public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
//}