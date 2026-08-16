using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Ecommerce.Application.RepoContracts;
using Ecommerce.Domain.Entities;
using Ecommerce.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Infrastructure.Repositories;

public class UserAddressesRepository : IUserAddressesRepository
{
    private readonly AppDbContext _context;

    public UserAddressesRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<UserAddresses?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.UserAddresses
            .Include(ua => ua.Country)
            .Include(ua => ua.State)
            .Include(ua => ua.City)
            .FirstOrDefaultAsync(ua => ua.Id == id && ua.DeletedAt == null, cancellationToken);
    }

    public async Task<List<UserAddresses>> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        return await _context.UserAddresses
            .Include(ua => ua.Country)
            .Include(ua => ua.State)
            .Include(ua => ua.City)
            .Where(ua => ua.UserId == userId && ua.DeletedAt == null)
            .OrderByDescending(ua => ua.IsDefault)
            .ThenByDescending(ua => ua.CreatedAt)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(UserAddresses userAddress, CancellationToken cancellationToken = default)
    {
        await _context.UserAddresses.AddAsync(userAddress, cancellationToken);
    }

    public void Update(UserAddresses userAddress)
    {
        _context.UserAddresses.Update(userAddress);
    }

    public void Delete(UserAddresses userAddress)
    {
        userAddress.DeletedAt = DateTime.UtcNow;
        _context.UserAddresses.Update(userAddress);
    }

    public async Task<UserAddresses?> GetDefaultAddressAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        return await _context.UserAddresses
            .FirstOrDefaultAsync(ua => ua.UserId == userId && ua.IsDefault && ua.DeletedAt == null, cancellationToken);
    }

    public async Task<bool> ValidateLocationAsync(Guid countryId, Guid stateId, Guid cityId, CancellationToken cancellationToken = default)
    {
        var countryExists = await _context.Countries
            .AnyAsync(c => c.Id == countryId && c.DeletedAt == null, cancellationToken);
        if (!countryExists) return false;

        var stateExists = await _context.States
            .AnyAsync(s => s.Id == stateId && s.CountryId == countryId && s.DeletedAt == null, cancellationToken);
        if (!stateExists) return false;

        var cityExists = await _context.Cities
            .AnyAsync(c => c.Id == cityId && c.StateId == stateId && c.DeletedAt == null, cancellationToken);
        
        return cityExists;
    }
}
