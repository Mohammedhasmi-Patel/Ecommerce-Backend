using Ecommerce.Application.RepoContracts;
using Ecommerce.Domain.Entities;
using Ecommerce.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Infrastructure.Repositories;

public class CitiesRepository : ICitiesRepository
{
    private readonly AppDbContext _context;

    public CitiesRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<City>> GetAllAsync(Guid? stateId, string? search, CancellationToken cancellationToken)
    {
        var query = _context.Cities.AsQueryable();

        if (stateId.HasValue)
        {
            query = query.Where(x => x.StateId == stateId.Value);
        }

        if (!string.IsNullOrEmpty(search))
        {
            query = query.Where(x => x.Name.ToLower().Contains(search.ToLower()));
        }

        return await query.ToListAsync(cancellationToken);
    }
}
