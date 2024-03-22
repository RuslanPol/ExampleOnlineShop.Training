using System.Net.Http.Json;
using ExampleOnlineShop.Models;
using HttpApiClient;

namespace ExampleOnlineShop.HttpApiClient;

public class ShopClient : IShopClient
{
    private const string DefaultHost = "https://api.mysite.com";
    private readonly string _host;
    private readonly HttpClient _httpClient;

    public ShopClient(string host = DefaultHost, HttpClient? httpClient = null)
    {
        _host = host;
        _httpClient = httpClient ?? new HttpClient();
    }

    public async Task<IReadOnlyCollection<Product>> GetProducts()
    {
        var uri = $"{_host}/products";
        IReadOnlyCollection<Product>? products =
            await _httpClient.GetFromJsonAsync<IReadOnlyCollection<Product>>(uri);
        return products!;
    }

    public async Task AddProduct(Product product)
    {
        if (product is null)
        {
            throw new ArgumentNullException(nameof(product));
        }

        var uri = $"{_host}/products";
        var responseMessage = await _httpClient.PostAsJsonAsync(uri, product);
        responseMessage.EnsureSuccessStatusCode();
    }

    public async Task<Product> GetProduct(long id)
    {
        var uri = $"{_host}/products/get_product?Id={id}";
        var product = await _httpClient.GetFromJsonAsync<Product>(uri);
        return product!;
    }

    public async Task UpdateProduct(long id, Product product)
    {
        var uri =$"{_host}/products/update_product?Id={id}";
        var responseMassage = await _httpClient.PutAsJsonAsync(uri, product);
        responseMassage.EnsureSuccessStatusCode();
    }

    public async Task DeleteProduct(long id)
    {
        var uri =$"{_host}/products/delete_product?Id={id}";
        var responseMassage=await _httpClient.DeleteAsync(uri);
        responseMassage.EnsureSuccessStatusCode();

    }
}