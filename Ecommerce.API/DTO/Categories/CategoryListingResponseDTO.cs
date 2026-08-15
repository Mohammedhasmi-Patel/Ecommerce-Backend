namespace Ecommerce.API.DTO.Categories;

public class CategoryListingResponseDTO
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string Slug { get; set; } = null!;
}
