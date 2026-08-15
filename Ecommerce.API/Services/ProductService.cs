using Ecommerce.API.DTO;
using Ecommerce.API.DTO.Common.Pagination;
using Ecommerce.API.DTO.Products;
using Ecommerce.API.Entities;
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
    public async Task< ApiResponse<PaginationResponseDto<ProductResponseDTO>>> GetAllAsync(ProductQueryParameters queryParameters)
    {
        PaginationResponseDto<Product> products = await _productRepository.GetAllAsync(queryParameters);

        var productsResponse = (products.Items ?? new List<Product>())
            .Select(p => p.MapToProductResponseDTO())
            .ToList();

        int totalPage = products.TotalPages;
        int totalCount = products.TotalCount;
        int page = products.PageNumber;
        int pageSize = products.PageSize;

        var res =  new PaginationResponseDto<ProductResponseDTO>
        {
            Items = productsResponse,
            TotalPages = totalPage,
            TotalCount = totalCount,
            PageNumber = page,
            PageSize = pageSize
        };
        return ApiResponse<PaginationResponseDto<ProductResponseDTO>>.SuccessResponse(res,"Product fetched successfully.");
    }

}
