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
    public async Task<PaginationResponseDTO<Product>> GetAllAsync(ProductQueryParameters queryParameters)
    {
        string search = queryParameters.SearchQuery?.Trim() ?? "";
        var productQuery = _context.Products
                                    .Include(p => p.ProductImages)
                                    .Include(p => p.Categories)
                                    .Where(p => p.DeletedAt == null)
                                    .AsQueryable();

        if (!string.IsNullOrEmpty(search))
        {
            productQuery = productQuery.Where(p =>
                p.Name.Contains(search) ||
                p.Description.Contains(search));
        }

        if (!string.IsNullOrEmpty(queryParameters.Category))
        {
            productQuery = productQuery.Where(p =>
                p.Categories.Any(c => c.Category != null && c.Category.Slug == queryParameters.Category));
        }

        var totalItem = await productQuery.CountAsync();

        var response = await productQuery
                                    .OrderByDescending(p => p.CreatedAt)
                                    .Skip((queryParameters.PageNumber - 1) * queryParameters.PageSize)
                                    .Take(queryParameters.PageSize)
                                    .ToListAsync();

        return new PaginationResponseDTO<Product>
        {
            Items = response,
            PageNumber = queryParameters.PageNumber,
            PageSize = queryParameters.PageSize,
            TotalCount = totalItem,
            TotalPages = (int)Math.Ceiling(totalItem / (double)queryParameters.PageSize)
        };

    }

    public async Task<Product> GetByIdAsync(Guid productId)
    {
        return await _context.Products
                            .Where(p => p.DeletedAt == null)
                            .FirstOrDefaultAsync(p => p.Id == productId);
    }

    public async Task<Product?> GetBySlugAsync(string slug)
    {
        return await _context.Products
                            .Where(p => p.DeletedAt == null)
                            .Include(p => p.ProductImages)
                            .FirstOrDefaultAsync(p => p.Slug == slug);

    }

}

