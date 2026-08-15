namespace Ecommerce.API.DTO.Products;

public class ProductResponseDTO
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string Slug { get; set; } = null!;
    public string Description { get; set; } = null!;

    public decimal OriginalPrice { get; set; }

    public decimal SellPrice { get; set; }
}
