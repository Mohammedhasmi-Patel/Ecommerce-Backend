using Ecommerce.API.DTO.Categories;
using Ecommerce.API.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.API.Controllers;

[Route("api/categories")]
[Authorize]
public class CategoryController : BaseController
{
    private readonly ICategoryService _categoryService;

    public CategoryController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }
    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] CategoryListingRequestDTO categoryRequest)
    {
        // return Ok();
        var response = await _categoryService.GetAllCategoriesAsync(categoryRequest);
        return Ok(response);
    }
}
