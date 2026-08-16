using Ecommerce.Domain.Entities;

namespace Ecommerce.Application.RepoContracts;

public interface IStatesRepository
{
    Task<List<State>> GetAllAsync(Guid? countryId, string? search, CancellationToken cancellationToken);
}
