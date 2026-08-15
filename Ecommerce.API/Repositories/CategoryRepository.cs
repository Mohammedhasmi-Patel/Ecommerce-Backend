using Ecommerce.API.Database;
using Ecommerce.API.DTO;
using Ecommerce.API.DTO.Categories;
using Ecommerce.API.Entities;
using Ecommerce.API.RepoContracts;
using Microsoft.EntityFrameworkCore;
using Ecommerce.API.DTO.Common.Pagination;


namespace Ecommerce.API.Repositories;

public class CategoryRepository : ICategoryRepository
{
    private readonly AppDbContext _context;

    public CategoryRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<PaginationResponseDTO<Category>> GetAllAsync(CommonCategoryFilterDTO categoryFilter)
    {
        // throw new NotImplementedException();
        var categoryQuery = _context.Categories.AsQueryable();

        if(!string.IsNullOrEmpty(categoryFilter.Search))
        {
            categoryQuery = categoryQuery.Where(c => c.Name.ToLower().Contains(categoryFilter.Search.ToLower()));
        }

        int totalCategories = await categoryQuery.CountAsync();


        var categories = await categoryQuery
            .Skip((categoryFilter.Page - 1) * categoryFilter.PageSize)
            .Take(categoryFilter.PageSize)
            .ToListAsync();

        return new PaginationResponseDTO<Category>
        {
            Items = categories,
            TotalCount = totalCategories,
            PageNumber = categoryFilter.Page,
            PageSize = categoryFilter.PageSize,
            TotalPages = (int)Math.Ceiling((double)totalCategories / categoryFilter.PageSize)
        };
    }

}
