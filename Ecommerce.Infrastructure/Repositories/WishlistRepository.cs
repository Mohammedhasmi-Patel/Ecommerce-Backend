using Ecommerce.Infrastructure.Database;
using Ecommerce.Domain.Entities;
using Ecommerce.Application.RepoContracts;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Infrastructure.Repositories;

public class WishlistRepository : IWishlistRepository
{
    private readonly AppDbContext _context;

    public WishlistRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Wishlist> AddWishlistAsync(Wishlist wishlist)
    {
        await _context.Wishlists.AddAsync(wishlist);
        return wishlist;
    }

    public async Task<WishlistItem> AddWishlistItemAsync(WishlistItem wishlistItem)
    {
        await _context.WishlistItems.AddAsync(wishlistItem);
        return wishlistItem;
    }

    public async Task<bool> DeleteWishlistItemAsync(Guid wishlistItemId)
    {
        var wishlistItem = await _context.WishlistItems.FirstOrDefaultAsync(wi => wi.Id == wishlistItemId);
        if (wishlistItem == null)
        {
            return false;
        }
        _context.WishlistItems.Remove(wishlistItem);
        return true;
    }

    public async Task<Wishlist?> GetWishlistByUserId(Guid userId)
    {
        return await _context.Wishlists
                    .Include(w => w.WishlistItems)
                        .ThenInclude(wi => wi.Product)
                        .ThenInclude(p => p.ProductImages)
                    .FirstOrDefaultAsync(w => w.UserId == userId);
    }
}
