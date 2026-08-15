using Ecommerce.API.DTO;
using Ecommerce.API.DTO.Categories;
using Ecommerce.API.DTO.Common.Pagination;
using Ecommerce.API.Entities;
using Ecommerce.API.Interfaces;
using Ecommerce.API.Mapster;
using Ecommerce.API.RepoContracts;

namespace Ecommerce.API.Services;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;
    public CategoryService(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }
    public async Task<ApiResponse<PaginationResponseDTO<CategoryListingResponseDTO>>> GetAllCategoriesAsync(CategoryListingRequestDTO categoryRequest)
    {
        PaginationResponseDTO<Category> categories = await _categoryRepository.GetAllAsync(categoryRequest.ToCommonCategoryFilterDTO());

        var categoriesResponse = (categories.Items ?? new List<Category>())
                                .Select(c => c.MapToCategoryListingResponseDTO())
                                .ToList();

        int totalPage = categories.TotalPages;
        int totalCount = categories.TotalCount;
        int page = categories.PageNumber;
        int pageSize = categories.PageSize;

        var categoryRes = new PaginationResponseDTO<CategoryListingResponseDTO>
        {
            Items = categoriesResponse,
            TotalPages = totalPage,
            TotalCount = totalCount,
            PageNumber = page,
            PageSize = pageSize
        };

        return ApiResponse<PaginationResponseDTO<CategoryListingResponseDTO>>.SuccessResponse(categoryRes, "Categories fetched successfully.");
    }
}
