using ExampleOnlineShop.Data;
using ExampleOnlineShop.Data.Genericrepository;
using ExampleOnlineShop.Data.Repositories;
using ExampleOnlineShop.Domain.InterfacesRepositories;
using ExampleOnlineShop.Domain.Services;
using ExampleOnlineShop.WebApi.Services;
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
        builder.Services.AddScoped<IProductRepository, ProductRepository>();
        builder.Services.AddScoped<IAccountRepository, AccountRepository>();
        builder.Services.AddScoped<AccountService>();
        builder.Services.AddSingleton<IPasswordHasherService, PasswordHasherService>();
      


        builder.Services.AddCors();


        var app = builder.Build();
        app.UseHttpsRedirection();
        app.MapControllers();
        // app.Use(async (context, next) =>
        //     {
        //         var userContext =  context.Request.Headers.UserAgent.ToString();
        //         if (userContext.Contains("Edg"))
        //         {
        //             await next();
        //         }
        //         else
        //         {
        //             await context.Response.WriteAsJsonAsync(new { message = "Go to Edge browser" });
        //         }
        //
        //     }
        // );
        
        app.UseCors(policy =>
        {
            policy
                .WithOrigins("https://localhost:7251", "https://mysite.ru")
                .AllowAnyHeader()
                .AllowAnyMethod()
                ;
        });

     
        
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        app.Run();
    }

    
}