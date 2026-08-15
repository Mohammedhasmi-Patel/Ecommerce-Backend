namespace Ecommerce.API.DTO.Carts;

public class UserCartItemResponseDTO
{
    public Guid CartItemId { get; set; }
    public Guid ProductId { get; set; }
    public string ProductName { get; set; }
    public string ProductImage { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public decimal TotalPrice { get => Price * Quantity; }
}
