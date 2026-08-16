namespace Ecommerce.Application.DTOs.Products;

public class ProductDetailResponseDTO
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string Slug { get; set; } = null!;
    public string Description { get; set; } = null!;
    public decimal OriginalPrice { get; set; }
    public decimal SellPrice { get; set; }

    public List<ProductMediaDTO> Media { get; set; } = [];
}

