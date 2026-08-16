namespace Ecommerce.Application.DTOs.Wishlists;

public class WishlisttemResponseDTO
{
    public Guid WishlistItemId { get; set; }
    public Guid ProductId { get; set; }
    public string ProductName { get; set; } = null!;
    public string ProductImage { get; set; } = null!;
    public decimal Price { get; set; }
}
