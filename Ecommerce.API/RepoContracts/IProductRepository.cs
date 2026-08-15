using Ecommerce.API.DTO;
using Ecommerce.API.DTO.Common.Pagination;
using Ecommerce.API.DTO.Products;
using Ecommerce.API.Entities;

namespace Ecommerce.API.RepoContracts;

public interface IProductRepository
{
    Task<PaginationResponseDto<Product?>> GetAllAsync(ProductQueryParameters queryParameters);
    // Task<ProductResponseDTO> GetByIdAsync(int productId);
    // Task<int> CreateProductAsync(ProductCreateDto productDto);
    // Task<bool> UpdateProductAsync(int productId, ProductUpdateDto productDto);
    // Task<bool> DeleteProductAsync(int productId);
}
