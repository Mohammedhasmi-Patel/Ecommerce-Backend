using Ecommerce.Infrastructure.Database;
using Ecommerce.Application.DTOs;
using Ecommerce.Application.DTOs.Categories;
using Ecommerce.Domain.Entities;
using Ecommerce.Application.RepoContracts;
using Microsoft.EntityFrameworkCore;
using Ecommerce.Application.DTOs.Common.Pagination;


namespace Ecommerce.Infrastructure.Repositories;

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

