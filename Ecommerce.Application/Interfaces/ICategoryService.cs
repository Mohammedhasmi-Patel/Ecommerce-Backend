using Ecommerce.Application.DTOs;
using Ecommerce.Application.DTOs.Categories;
using Ecommerce.Application.DTOs.Common.Pagination;

namespace Ecommerce.Application.Interfaces;

public interface ICategoryService 
{
    public Task<ApiResponse<PaginationResponseDTO<CategoryListingResponseDTO>>> GetAllCategoriesAsync(CategoryListingRequestDTO categoryRequest);
}

