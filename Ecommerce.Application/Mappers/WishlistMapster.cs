using Ecommerce.Application.DTOs.Wishlists;
using Ecommerce.Domain.Entities;

namespace Ecommerce.Application.Mappers;

public static class WishlistMapster
{
    public static Wishlist ToWishlist(this AddToWishListRequestDTO request, Guid userId)
    {
        return new Wishlist
        {
            UserId = userId,
            WishlistItems = new List<WishlistItem>
            {
                new WishlistItem
                {
                    ProductId = request.ProductId
                }
            }
        };
    }
}
