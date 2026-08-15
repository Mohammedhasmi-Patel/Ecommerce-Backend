using Ecommerce.API.Database;
using Ecommerce.API.DTO;
using Ecommerce.API.DTO.Common.Pagination;
using Ecommerce.API.DTO.Products;
using Ecommerce.API.Entities;
using Ecommerce.API.RepoContracts;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.API.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly AppDbContext _context;

    public ProductRepository(AppDbContext context)
    {
        _context = context;
    }
    public async Task<PaginationResponseDto<Product>> GetAllAsync(ProductQueryParameters queryParameters)
    {
        string search = queryParameters.SearchQuery?.Trim() ?? "";
        var productQuery = _context.Products
                                    .Include(p => p.ProductImages)
                                    .Where(p => p.DeletedAt == null)
                                    .AsQueryable();

        if (!string.IsNullOrEmpty(search))
        {
            productQuery = productQuery.Where(p =>
                p.Name.Contains(search) ||
                p.Description.Contains(search));
        }

        var totalItem = await productQuery.CountAsync();

        var response = await productQuery
                                    .OrderByDescending(p => p.CreatedAt)
                                    .Skip((queryParameters.PageNumber - 1) * queryParameters.PageSize)
                                    .Take(queryParameters.PageSize)
                                    .ToListAsync();

        return new PaginationResponseDto<Product>
        {
            Items = response,
            PageNumber = queryParameters.PageNumber,
            PageSize = queryParameters.PageSize,
            TotalCount = totalItem,
            TotalPages = (int)Math.Ceiling(totalItem / (double)queryParameters.PageSize)
        };

    }

}

