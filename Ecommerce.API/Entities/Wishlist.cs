namespace Ecommerce.API.Entities;

/*
Table Wishlists {
  Id Guid [PRIMARY KEY]
  UserId Guid [UNIQUE]

  CreatedAt timestamp
  UpdatedAt timestamp
}

Table WishlistItems {
  Id Guid [PRIMARY KEY]

  WishlistId Guid
  ProductId Guid

  CreatedAt timestamp

}

*/

public class Wishlist
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; }
}
