namespace Ecommerce.API.DTO.Categories;

public class CommonCategoryFilterDTO
{
    public string? Search { get; set; }
    public int PageSize { get; set; } = 10;
    public int Page { get; set; } = 1;
    
}
