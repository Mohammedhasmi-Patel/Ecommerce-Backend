using System.ComponentModel.DataAnnotations.Schema;

namespace Ecommerce.API.Entities;

public class Product : BaseEntity
{
    public string Name { get; set; } = null!;
    public string Slug { get; set; } = null!;
    public string Description { get; set; } = null!;

    [Column(TypeName = "decimal(10,2)")]
    public decimal OriginalPrice { get; set; }

    [Column(TypeName = "decimal(10,2)")]
    public decimal SellPrice { get; set; }
    public decimal StockQuantity { get; set; }

    public ICollection<ProductImage> ProductImages { get; set; } = [];
}
