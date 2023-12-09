using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using kds.Models;

namespace kds.Controllers;

[Route("api/products")]
[ApiController]
public class ProductsAPIController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductsAPIController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpGet]
    public IActionResult GetProducts(string query = "", int page = 1)
    {
        var products = _productService.GetProducts(query, page);
        return Ok(products);
    }

}

