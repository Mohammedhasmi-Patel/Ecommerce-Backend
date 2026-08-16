using System.Security.Claims;
using Ecommerce.Application.DTOs.Wishlists;
using Ecommerce.Domain.Exceptions;
using Ecommerce.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.API.Controllers;

[Route("api/wishlists")]
public class WishlistController : BaseController
{
    private readonly IWishlistService _wishlistService;

    public WishlistController(IWishlistService wishlistService)
    {
        _wishlistService = wishlistService;
    }

    [HttpPost]
    public async Task<IActionResult> AddToWishlist([FromBody] AddToWishListRequestDTO wishlistRequest)
    {
        var userEmail = User.FindFirstValue(ClaimTypes.Email);
        if (string.IsNullOrEmpty(userEmail))
        {
            throw new UnauthorizedException("User not authenticated");
        }

        var response = await _wishlistService.AddWishlistItemAsync(userEmail, wishlistRequest);
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetWishlistByUser()
    {
        var userEmail = User.FindFirstValue(ClaimTypes.Email);
        if (string.IsNullOrEmpty(userEmail))
        {
            throw new UnauthorizedException("User not authenticated");
        }

        var response = await _wishlistService.GetWishlistByUserAsync(userEmail);
        return Ok(response);
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteWishlistItem(Guid wishlistItemId)
    {
        var userEmail = User.FindFirstValue(ClaimTypes.Email);
        if (string.IsNullOrEmpty(userEmail))
        {
            throw new UnauthorizedException("User not authenticated");
        }

        var response = await _wishlistService.DeleteWishlistItemAsync(userEmail, wishlistItemId);
        return Ok(response);
    }
}
