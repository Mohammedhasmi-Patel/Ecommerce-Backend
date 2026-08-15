using Ecommerce.API.DTO.Categories;
using Ecommerce.API.Entities;

namespace Ecommerce.API.Mapster;

public static class CategoryMapster
{
    public static CommonCategoryFilterDTO ToCommonCategoryFilterDTO(this CategoryListingRequestDTO categoryRequest)
    {
        return new CommonCategoryFilterDTO
        {
            Page = categoryRequest.Page,
            PageSize = categoryRequest.PageSize,
            Search = categoryRequest.Search,
        };
    }

    public static CategoryListingResponseDTO MapToCategoryListingResponseDTO(this Category category)
    {
        return new CategoryListingResponseDTO
        {
            Id = category.Id,
            Name = category.Name,
            Slug = category.Slug,
        };
    }
    
}
