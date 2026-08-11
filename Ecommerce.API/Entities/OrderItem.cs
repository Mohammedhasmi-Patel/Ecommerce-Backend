namespace Ecommerce.API.Entities;

public class OrderItem : BaseEntity
{
    public Guid OrderId { get; set; }
    public Guid ProductId { get; set; }
    public string ProductName { get; set; } = null!;
    public decimal UnitPrice { get; set; }
    public decimal Quantity { get; set; }
    public decimal TotalPrice { get; set; }

    // navigation property
    public Order Order { get; set; } = null!;
    public Product Product { get; set; } = null!;
}
