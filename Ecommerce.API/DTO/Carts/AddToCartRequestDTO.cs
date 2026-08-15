namespace Ecommerce.API.DTO.Carts;

public class AddToCartRequestDTO
{
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
}
