namespace Ecommerce.API.DTO.Categories;

public class CategoryListingRequestDTO
{
    public string? Search { get; set; }
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}
