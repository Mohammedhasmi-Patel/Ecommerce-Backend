namespace Ecommerce.Application.DTOs.Carts;

public class AddToCartRequestDTO
{
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
}

