// See https://aka.ms/new-console-template for more information

using ExampleOnlineShop.HttpApiClient;
using ExampleOnlineShop.Models;
using ExampleShop.Entities;
using HttpApiClient;

Console.WriteLine("Hello, World!");
var shopClient = new ShopClient("https://localhost:7003");
 await shopClient.AddProduct(new Product(7,"bread",100m,100,"bla-bla","/"));
 var products = await shopClient.GetProducts();
foreach (var product in products)
{
    Console.WriteLine(product.Name);
}