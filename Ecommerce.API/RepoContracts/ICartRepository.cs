using Ecommerce.API.Entities;

namespace Ecommerce.API.RepoContracts;

public interface ICartRepository
{
    public Task<Cart> AddCartAsync(Cart cart);
    public Task<Cart> GetCartByUserId(Guid userId);
    public Task<CartItem> AddToCartItemAsync(CartItem cartItem);
    public Task<CartItem> UpdateCartItemAsync(CartItem cartItem);
    public Task<bool> DeleteCartItemAsync(Guid cartItemId);
}
