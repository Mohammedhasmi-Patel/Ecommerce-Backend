
using Ecommerce.Domain.Entities;

namespace Ecommerce.Application.RepoContracts;

public interface IUserAddressesRepository
{
    Task<UserAddresses?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<List<UserAddresses>> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);
    Task AddAsync(UserAddresses userAddress, CancellationToken cancellationToken = default);
    void Update(UserAddresses userAddress);
    void Delete(UserAddresses userAddress);
    Task<UserAddresses?> GetDefaultAddressAsync(Guid userId, CancellationToken cancellationToken = default);
    Task<bool> ValidateLocationAsync(Guid countryId, Guid stateId, Guid cityId, CancellationToken cancellationToken = default);
}
