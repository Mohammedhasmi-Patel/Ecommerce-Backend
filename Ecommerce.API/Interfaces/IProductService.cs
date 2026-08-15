using Ecommerce.API.DTO;
using Ecommerce.API.DTO.Common.Pagination;
using Ecommerce.API.DTO.Products;

namespace Ecommerce.API.Interfaces;

public interface IProductService
{
    Task<ApiResponse<PaginationResponseDTO<ProductResponseDTO>>> GetAllAsync(ProductQueryParameters queryParameters);
    Task<ApiResponse<ProductDetailResponseDTO>> GetBySlugAsync(string slug);
}
