namespace Ecommerce.Application.DTOs.Categories;

public class CategoryListingResponseDTO
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string Slug { get; set; } = null!;
}

