using Ecommerce.API.DTO.Products;
using Ecommerce.API.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.API.Controllers;

[Route("api/products")]
[Authorize]
public class ProductController : BaseController
{
    private readonly IProductService _productService;
    public ProductController(IProductService productService)
    {
        _productService = productService;
    }
    
    [HttpGet]
    public async Task<IActionResult> Get(ProductQueryParameters queryParameters)
    {
         var response = await _productService.GetAllAsync(queryParameters);
         return Ok(response);
    }
}
