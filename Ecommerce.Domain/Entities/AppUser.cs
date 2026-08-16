using Microsoft.AspNetCore.Identity;

namespace Ecommerce.Domain.Entities;

public class AppUser : IdentityUser<Guid>
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string? Avatar { get; set; }
    public bool IsActive { get; set; } = true;

    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }

    // navigations propertly 

    public ICollection<Order> Orders { get; set; } = [];
    public ICollection<UserAddresses> UserAddresses { get; set; } = [];
    public ICollection<Wishlist> Wishlists { get; set; } = [];
    public ICollection<Cart> Carts { get; set; } = [];
}

