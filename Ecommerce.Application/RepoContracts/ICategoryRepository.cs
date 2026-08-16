using Ecommerce.Application.DTOs;
using Ecommerce.Application.DTOs.Categories;
using Ecommerce.Application.DTOs.Common.Pagination;
using Ecommerce.Domain.Entities;

namespace Ecommerce.Application.RepoContracts;

public interface ICategoryRepository
{
    public Task<PaginationResponseDTO<Category>> GetAllAsync(CommonCategoryFilterDTO categoryFilter);
}

