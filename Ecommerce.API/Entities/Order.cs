using Ecommerce.API.Enum;

namespace Ecommerce.API.Entities;



public class Order : BaseEntity
{
    public Guid UserId { get; set; }
    public string OrderNote { get; set; } = null!;
    public string OrderNumber { get; set; } = null!;
    public decimal SubTotal { get; set; }
    public decimal DiscountAmount { get; set; }
    public decimal ShippingAmount { get; set; }
    public decimal TaxAmount { get; set; }
    public decimal TotalAmount { get; set; }
    public OrderStatus Status { get; set; } 
}
