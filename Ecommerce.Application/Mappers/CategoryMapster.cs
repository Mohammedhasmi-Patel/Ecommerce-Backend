using Ecommerce.Application.DTOs.Categories;
using Ecommerce.Domain.Entities;

namespace Ecommerce.Application.Mappers;

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

