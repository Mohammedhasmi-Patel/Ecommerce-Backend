using Ecommerce.API.DTO;
using Ecommerce.API.DTO.Common.Pagination;
using Ecommerce.API.DTO.Products;
using Ecommerce.API.Entities;

namespace Ecommerce.API.RepoContracts;

public interface IProductRepository
{
    Task<PaginationResponseDTO<Product>> GetAllAsync(ProductQueryParameters queryParameters);
    Task<Product?> GetByIdAsync(Guid productId);
    Task<Product?> GetBySlugAsync(string slug);
    // Task<int> CreateProductAsync(ProductCreateDto productDto);
    // Task<bool> UpdateProductAsync(int productId, ProductUpdateDto productDto);
    // Task<bool> DeleteProductAsync(int productId);
}
