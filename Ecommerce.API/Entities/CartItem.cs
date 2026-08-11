namespace Ecommerce.API.Entities;

public class CartItem
{
    public Guid Id { get; set; }
    public Guid CartId { get; set; }
    public Guid ProductId { get; set; }
    public decimal Quantity { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; }

    // navigation properties
    public Cart Cart { get; set; } = null!;
    public Product Product { get; set; } = null!;
}
