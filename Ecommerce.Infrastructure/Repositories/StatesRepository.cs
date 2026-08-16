using Ecommerce.Application.RepoContracts;
using Ecommerce.Domain.Entities;
using Ecommerce.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Infrastructure.Repositories;

public class StatesRepository : IStatesRepository
{
    private readonly AppDbContext _context;

    public StatesRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<State>> GetAllAsync(Guid? countryId, string? search, CancellationToken cancellationToken)
    {
        var query = _context.States.AsQueryable();

        if (countryId.HasValue)
        {
            query = query.Where(x => x.CountryId == countryId.Value);
        }

        if (!string.IsNullOrEmpty(search))
        {
            query = query.Where(x => x.Name.ToLower().Contains(search.ToLower()));
        }

        return await query.ToListAsync(cancellationToken);
    }
}
