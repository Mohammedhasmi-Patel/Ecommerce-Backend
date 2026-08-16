namespace Ecommerce.Application.DTOs.Carts;

public class UserCartItemResponseDTO
{
    public Guid CartItemId { get; set; }
    public Guid ProductId { get; set; }
    public string ProductName { get; set; } = null!;
    public string ProductImage { get; set; } = null!;
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public decimal TotalPrice { get => Price * Quantity; }
}

