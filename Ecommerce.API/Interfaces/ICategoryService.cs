using Ecommerce.API.DTO;
using Ecommerce.API.DTO.Categories;
using Ecommerce.API.DTO.Common.Pagination;

namespace Ecommerce.API.Interfaces;

public interface ICategoryService 
{
    public Task<ApiResponse<PaginationResponseDTO<CategoryListingResponseDTO>>> GetAllCategoriesAsync(CategoryListingRequestDTO categoryRequest);
}
