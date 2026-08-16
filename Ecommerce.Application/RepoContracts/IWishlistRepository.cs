using Ecommerce.Domain.Entities;

namespace Ecommerce.Application.RepoContracts;

public interface IWishlistRepository
{
    public Task<Wishlist> AddWishlistAsync(Wishlist wishlist);
    public Task<Wishlist?> GetWishlistByUserId(Guid userId);
    public Task<WishlistItem> AddWishlistItemAsync(WishlistItem wishlistItem);
    public Task<bool> DeleteWishlistItemAsync(Guid wishlistItemId);
}
