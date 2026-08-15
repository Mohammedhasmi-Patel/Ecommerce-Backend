using System.Security.Claims;
using Ecommerce.API.DTO.Carts;
using Ecommerce.API.Exceptions;
using Ecommerce.API.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.API.Controllers;

[ApiController]
[Route("api/carts")]
public class CartController : BaseController
{
    private readonly ICartService _cartService;

    public CartController(ICartService cartService)
    {
        _cartService = cartService;
    }

    [HttpPost]
    public async Task<IActionResult> AddToCart([FromBody] AddToCartRequestDTO cartRequest)
    {
        var userEmail = User.FindFirstValue(ClaimTypes.Email);
        if (string.IsNullOrEmpty(userEmail))
        {
            throw new UnauthorizedException("User not authenticated");
        }

        var response = await _cartService.AddCartItemAsync(userEmail, cartRequest);
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetCartByUser()
    {
        var userEmail = User.FindFirstValue(ClaimTypes.Email);
        if (string.IsNullOrEmpty(userEmail))
        {
            throw new UnauthorizedException("User not authenticated");
        }

        var response = await _cartService.GetCartByUser(userEmail);
        return Ok(response);
    }

    
    [HttpDelete]
    public async Task<IActionResult> DeleteCartItem(Guid cartItemId)
    {
        var userEmail = User.FindFirstValue(ClaimTypes.Email);
        var response = await _cartService.DeleteCartItemAsync(userEmail, cartItemId);
        return Ok(response);
    }

}
