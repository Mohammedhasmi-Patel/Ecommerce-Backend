using Ecommerce.Application.DTOs;
using Ecommerce.Application.DTOs.Carts;

namespace Ecommerce.Application.Interfaces;

public interface ICartService
{
    public Task<ApiResponse<UserCartItemResponseDTO>> AddCartItemAsync(string email, AddToCartRequestDTO cartRequest);
    public Task<ApiResponse<List<UserCartItemResponseDTO>>> GetCartByUser(string email);
    public Task<ApiResponse<object>> DeleteCartItemAsync(string email, Guid cartItemId);
    // public async Task<ApiResponse> UpdateCartItemAsync(Guid userId, UpdateCartItemRequestDTO cartRequest);
    // public async Task<ApiResponse> DeleteCartItemAsync(Guid userId, Guid cartItemId);
}

