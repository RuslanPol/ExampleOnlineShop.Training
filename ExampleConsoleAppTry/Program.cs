// See https://aka.ms/new-console-template for more information

using ExampleOnlineShop.HttpApiClient;
using ExampleOnlineShop.Models;

Console.WriteLine("Hello, World!");
var shopClient = new ShopClient("https://localhost:7003");
 await shopClient.AddProduct(new Product(Guid.NewGuid(),"bread",100m,100,"bla-bla","/"),default);
 var products = await shopClient.GetProducts();
foreach (var product in products)
{
    Console.WriteLine(product.Name);
}