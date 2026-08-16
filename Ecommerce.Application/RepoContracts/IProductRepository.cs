using Ecommerce.Application.DTOs;
using Ecommerce.Application.DTOs.Common.Pagination;
using Ecommerce.Application.DTOs.Products;
using Ecommerce.Domain.Entities;

namespace Ecommerce.Application.RepoContracts;

public interface IProductRepository
{
    Task<PaginationResponseDTO<Product>> GetAllAsync(ProductQueryParameters queryParameters);
    Task<Product?> GetByIdAsync(Guid productId);
    Task<Product?> GetBySlugAsync(string slug);
    // Task<int> CreateProductAsync(ProductCreateDto productDto);
    // Task<bool> UpdateProductAsync(int productId, ProductUpdateDto productDto);
    // Task<bool> DeleteProductAsync(int productId);
}

