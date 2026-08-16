using Ecommerce.Domain.Entities;

namespace Ecommerce.Application.RepoContracts;

public interface ICountriesRepository 
{
    public Task<List<Country>> GetAllAsync(string? search,CancellationToken cancellationToken);
}
