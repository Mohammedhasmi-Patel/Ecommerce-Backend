using Ecommerce.Application.RepoContracts;
using Ecommerce.Domain.Entities;
using Ecommerce.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Infrastructure.Repositories;

public class CountriesRepository : ICountriesRepository
{
    private readonly AppDbContext _context;

    public CountriesRepository(AppDbContext context)
    {
        _context = context;
    }
    public async Task<List<Country>> GetAllAsync(string? search, CancellationToken cancellationToken)
    {
        // throw new NotImplementedException();
        var countryQuery = _context.Countries.AsQueryable();
        if (!string.IsNullOrEmpty(search))
        {
            countryQuery = countryQuery.Where(x => x.Name.ToLower().Contains(search.ToLower()));
        }

        var countryList = await countryQuery.ToListAsync(cancellationToken);
        return countryList;

    }

}
