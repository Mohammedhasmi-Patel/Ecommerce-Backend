using Ecommerce.Infrastructure.Database;
using Ecommerce.Domain.Entities;
using Ecommerce.Application.RepoContracts;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Infrastructure.Repositories;

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
        return cart;
    }

    public async Task<CartItem> AddToCartItemAsync(CartItem cartItem)
    {
        await _context.CartItems.AddAsync(cartItem);
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

    public Task<CartItem> UpdateCartItemAsync(CartItem cartItem)
    {
        _context.CartItems.Update(cartItem);
        return Task.FromResult(cartItem);
    }
}

