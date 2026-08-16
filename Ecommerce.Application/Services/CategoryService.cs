using Ecommerce.Application.DTOs;
using Ecommerce.Application.DTOs.Categories;
using Ecommerce.Application.DTOs.Common.Pagination;
using Ecommerce.Domain.Entities;
using Ecommerce.Application.Interfaces;
using Ecommerce.Application.Mappers;
using Ecommerce.Application.RepoContracts;

namespace Ecommerce.Application.Services;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CategoryService(ICategoryRepository categoryRepository, IUnitOfWork unitOfWork)
    {
        _categoryRepository = categoryRepository;
        _unitOfWork = unitOfWork;
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

