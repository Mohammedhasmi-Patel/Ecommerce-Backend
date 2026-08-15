using Ecommerce.API.DTO;
using Ecommerce.API.DTO.Common.Pagination;
using Ecommerce.API.DTO.Products;
using Ecommerce.API.Entities;
using Ecommerce.API.Exceptions;
using Ecommerce.API.Interfaces;
using Ecommerce.API.Mapster;
using Ecommerce.API.RepoContracts;

namespace Ecommerce.API.Services;


public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;

    public ProductService(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }
    public async Task<ApiResponse<PaginationResponseDTO<ProductResponseDTO>>> GetAllAsync(ProductQueryParameters queryParameters)
    {
        PaginationResponseDTO<Product> products = await _productRepository.GetAllAsync(queryParameters);

        var productsResponse = (products.Items ?? new List<Product>())
            .Select(p => p.MapToProductResponseDTO())
            .ToList();

        int totalPage = products.TotalPages;
        int totalCount = products.TotalCount;
        int page = products.PageNumber;
        int pageSize = products.PageSize;

        var res = new PaginationResponseDTO<ProductResponseDTO>
        {
            Items = productsResponse,
            TotalPages = totalPage,
            TotalCount = totalCount,
            PageNumber = page,
            PageSize = pageSize
        };
        return ApiResponse<PaginationResponseDTO<ProductResponseDTO>>.SuccessResponse(res, "Product fetched successfully.");
    }

    public async Task<ApiResponse<ProductDetailResponseDTO>> GetBySlugAsync(string slug)
    {
        var product = await _productRepository.GetBySlugAsync(slug) ?? throw new NotFoundException("Product not found.");
        ProductDetailResponseDTO productDetail = product.MapToProductDetailResponseDTO();
        return ApiResponse<ProductDetailResponseDTO>.SuccessResponse(productDetail, "Product fetched successfully.");
    }
}
