using Ecommerce.API.DTO;
using Ecommerce.API.DTO.Common.Pagination;
using Ecommerce.API.DTO.Products;

namespace Ecommerce.API.Interfaces;

public interface IProductService
{
    Task<ApiResponse<PaginationResponseDto<ProductResponseDTO>>> GetAllAsync(ProductQueryParameters queryParameters);
}
