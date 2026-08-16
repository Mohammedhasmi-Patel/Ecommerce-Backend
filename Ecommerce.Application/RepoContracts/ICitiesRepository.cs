using Ecommerce.Domain.Entities;

namespace Ecommerce.Application.RepoContracts;

public interface ICitiesRepository
{
    Task<List<City>> GetAllAsync(Guid? stateId, string? search, CancellationToken cancellationToken);
}
