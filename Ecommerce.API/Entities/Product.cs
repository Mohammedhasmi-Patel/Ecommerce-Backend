namespace Ecommerce.API.Entities;


/*
Table Products {
  Id Guid [PRIMARY KEY]
  Name string 
  Slug string
  Description string 
  Price decimal
  ThroughPrice decimal
  StockQuantity decimal

  IsActive boolean
  CreatedAt timestamp
  UpdatedAt timestamp
  DeletedAt timestamp
}

*/
public class Product : BaseEntity
{
    public string Name { get; set; } = null!;
    public string Slug { get; set; } = null!;
    public string Description { get; set; } = null!;
    public decimal Price { get; set; }
    public decimal ThroughPrice { get; set; }
    public decimal StockQuantity { get; set; }

}
