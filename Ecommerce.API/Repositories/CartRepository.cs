using Ecommerce.API.Database;
using Ecommerce.API.Entities;
using Ecommerce.API.RepoContracts;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.API.Repositories;

public class CartRepository : ICartRepository
{
    private readonly AppDbContext _context;

    public CartRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Cart> AddCartAsync(Cart cart)
    {
        await _context.Carts.AddAsync(cart);
        await _context.SaveChangesAsync();
        return cart;
    }

    public async Task<CartItem> AddToCartItemAsync(CartItem cartItem)
    {
        await _context.CartItems.AddAsync(cartItem);
        await _context.SaveChangesAsync();
        return cartItem;
    }

    public async Task<bool> DeleteCartItemAsync(Guid cartItemId)
    {
        var cartItem = await _context.CartItems.FirstOrDefaultAsync(ci => ci.Id == cartItemId);
        if (cartItem == null)
        {
            return false;
        }
        _context.CartItems.Remove(cartItem);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<Cart> GetCartByUserId(Guid userId)
    {
        return await _context.Carts
                    .Include(c => c.CartItems)
                        .ThenInclude(ci => ci.Product)
                        .ThenInclude(p => p.ProductImages)
                    .FirstOrDefaultAsync(c => c.UserId == userId);
    }

    public async Task<CartItem> UpdateCartItemAsync(CartItem cartItem)
    {
        _context.CartItems.Update(cartItem);
        await _context.SaveChangesAsync();
        return cartItem;
    }
}
