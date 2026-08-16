using Ecommerce.Application.DTOs;
using Ecommerce.Application.DTOs.Wishlists;

namespace Ecommerce.Application.Interfaces;

public interface IWishlistService
{
    Task<ApiResponse<WishlisttemResponseDTO>> AddWishlistItemAsync(string email, AddToWishListRequestDTO wishlistRequest);
    Task<ApiResponse<List<WishlisttemResponseDTO>>> GetWishlistByUserAsync(string email);
    Task<ApiResponse<object>> DeleteWishlistItemAsync(string email, Guid wishlistItemId);
}
