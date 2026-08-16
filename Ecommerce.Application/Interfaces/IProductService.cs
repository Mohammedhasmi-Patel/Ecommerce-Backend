using Ecommerce.Application.DTOs;
using Ecommerce.Application.DTOs.Common.Pagination;
using Ecommerce.Application.DTOs.Products;

namespace Ecommerce.Application.Interfaces;

public interface IProductService
{
    Task<ApiResponse<PaginationResponseDTO<ProductResponseDTO>>> GetAllAsync(ProductQueryParameters queryParameters);
    Task<ApiResponse<ProductDetailResponseDTO>> GetBySlugAsync(string slug);
}

