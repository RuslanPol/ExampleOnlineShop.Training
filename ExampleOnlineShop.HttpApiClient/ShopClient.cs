using System.Net.Http.Json;
using ExampleOnlineShop.Domain;
using ExampleOnlineShop.Models;

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

    public async Task<IReadOnlyCollection<Product>> GetProducts(CancellationToken cancellationToken= default)
    {
        var uri = $"{_host}/products/get_all";
        IReadOnlyCollection<Product>? products =
            await _httpClient.GetFromJsonAsync<IReadOnlyCollection<Product>>(uri,cancellationToken);
        return products!;
    }

   

    public async Task AddProduct(Product product, CancellationToken cancellationToken = default)
    {
        if (product is null)
        {
            throw new ArgumentNullException(nameof(product));
        }

        var uri = $"{_host}/products/add_product";
        var responseMessage = await _httpClient.PostAsJsonAsync(uri, product, cancellationToken);
        responseMessage.EnsureSuccessStatusCode();
    }

    public async Task<Product?> GetProduct(Guid id, CancellationToken cancellationToken = default)
    {
        var uri = $"{_host}/products/get_product?Id={id}";
        Product? product = await _httpClient.GetFromJsonAsync<Product>(uri, cancellationToken);
        return product!;
    }

    public async Task UpdateProduct(Guid id, Product product,CancellationToken cancellationToken=default)
    {
        var uri = $"{_host}/products/update_product?Id={id}";
        var responseMassage = await _httpClient.PutAsJsonAsync(uri, product,cancellationToken);
        responseMassage.EnsureSuccessStatusCode();
    }

    public async Task DeleteProduct(Guid id,CancellationToken cancellationToken = default)
    {
        var uri = $"{_host}/products/delete_product?Id={id}";
        var responseMassage = await _httpClient.DeleteAsync(uri,cancellationToken);
        responseMassage.EnsureSuccessStatusCode();
    }
    public async Task<IReadOnlyCollection<Account>> GetAccounts(CancellationToken cancellationToken= default)
    {
        var uri = $"{_host}/accounts/get_all";
        IReadOnlyCollection<Account>? accounts =
            await _httpClient.GetFromJsonAsync<IReadOnlyCollection<Account>>(uri,cancellationToken);
        return accounts!;
    }

    public async Task AddAccount(Account account, CancellationToken cancellationToken)
    {
        var uri =$"{_host}/accounts/register";
        await _httpClient.PostAsJsonAsync(uri, account, cancellationToken);
    }
}