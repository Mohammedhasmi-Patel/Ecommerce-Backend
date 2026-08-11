namespace Ecommerce.API.Entities;

public class ProductCategory : BaseEntity
{
    public Guid ProductId { get; set; }
    public Guid CategoryId { get; set; }

    public virtual Product? Product { get; set; }
    public virtual Category? Category { get; set; }
}
