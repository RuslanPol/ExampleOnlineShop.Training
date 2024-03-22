using ExampleOnlineShop.Models;

namespace ExampleOnlineShop.HttpApiClient.Test1;

public class UnitTest1
{
    [Fact]
    public async Task One_product_added_to_catalog()
    {
        
        var shopClient = new ShopClient("https://localhost:7003");
        var product = new Product( 15, "mil", 100m, 100, "bla-bla", "/");
        //await shopClient.DeleteProduct(product.Id);
        await shopClient.AddProduct(product);
        var responseProduct = await shopClient.GetProduct(product.Id);
        await shopClient.DeleteProduct(product.Id);
        Assert.Equal(product,responseProduct);
     

    }
}