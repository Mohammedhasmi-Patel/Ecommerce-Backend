namespace Ecommerce.API.Entities;

/*
Table OrderItems {
  Id Guid [PRIMARY KEY]
  OrderId Guid [NOT NULL]
  ProductId Guid

  ProductName string
  UnitPrice decimal
  Quantity decimal
  TotalPrice decimal

  CreatedAt timestamp
  UpdatedAt timestamp

}


*/

public class OrderItem : BaseEntity
{
    public Guid OrderId { get; set; }
    public Guid ProductId { get; set; }
    public string ProductName { get; set; } = null!;
    public decimal UnitPrice { get; set; }
    public decimal Quantity { get; set; }
    public decimal TotalPrice { get; set; }
}
