using Ecommerce.Application.DTOs;
using Ecommerce.Application.DTOs.Common.Pagination;
using Ecommerce.Application.DTOs.Products;
using Ecommerce.Domain.Entities;
using Ecommerce.Domain.Exceptions;
using Ecommerce.Application.Interfaces;
using Ecommerce.Application.Mappers;
using Ecommerce.Application.RepoContracts;

namespace Ecommerce.Application.Services;


public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;
    private readonly IUnitOfWork _unitOfWork;

    public ProductService(IProductRepository productRepository, IUnitOfWork unitOfWork)
    {
        _productRepository = productRepository;
        _unitOfWork = unitOfWork;
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

