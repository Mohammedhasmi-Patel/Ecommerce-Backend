namespace Ecommerce.API.Entities;


public class Wishlist
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; }

    // navigation property 
    public AppUser User { get; set; } = null!;
    public ICollection<WishlistItem> WishlistItems { get; set; } = [];

}
