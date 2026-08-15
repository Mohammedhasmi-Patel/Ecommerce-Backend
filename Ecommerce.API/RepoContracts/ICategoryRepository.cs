using Ecommerce.API.DTO;
using Ecommerce.API.DTO.Categories;
using Ecommerce.API.DTO.Common.Pagination;
using Ecommerce.API.Entities;

namespace Ecommerce.API.RepoContracts;

public interface ICategoryRepository
{
    public Task<PaginationResponseDTO<Category>> GetAllAsync(CommonCategoryFilterDTO categoryFilter);
}
