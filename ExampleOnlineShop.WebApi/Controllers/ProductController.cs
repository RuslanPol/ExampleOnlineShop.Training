using ExampleOnlineShop.Data.Genericrepository;
using ExampleOnlineShop.Domain.Entities;
using ExampleOnlineShop.Domain.InterfacesRepositories;
using Microsoft.AspNetCore.Mvc;

namespace ExampleOnlineShop.WebApi.Controllers;

[Route("products")]
public class ProductController : ControllerBase
{
    private readonly IRepository<Product> _productRepository;

    public ProductController(IRepository<Product> productRepository)
    {
        _productRepository = productRepository;
    }

    [HttpGet("get_all")]
    public async Task<IReadOnlyCollection<Product>> GetAll(CancellationToken cancellationToken)
    {
        var _products = await _productRepository.GetAll(cancellationToken);
        return _products;
    }

    [HttpGet($"get_product")]
    public async Task<Product> GetById([FromQuery]Guid id, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetById(id, cancellationToken);
        return product;
    }

    [HttpPost("add_product")]
    public async Task Add(Product product, CancellationToken cancellationToken)
    {
        await _productRepository.Add(product, cancellationToken);
    }

    [HttpPut("update_product")]
    public async Task Update([FromBody]Product product, CancellationToken cancellationToken)
    {
        await _productRepository.Update(product, cancellationToken);
    }

    [HttpDelete("delete_product")]
    public async Task DeleteById([FromQuery]Guid id, CancellationToken cancellationToken)
    {
        await _productRepository.DeleteById(id, cancellationToken);
    }
}