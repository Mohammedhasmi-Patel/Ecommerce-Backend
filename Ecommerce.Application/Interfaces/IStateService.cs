using Ecommerce.Application.DTOs;
using Ecommerce.Application.DTOs.States;

namespace Ecommerce.Application.Interfaces;

public interface IStateService
{
    Task<ApiResponse<List<StateListDropdownResponseDTO>>> GetStatesListDropdownAsync(Guid? countryId, string? search, CancellationToken cancellationToken);
}
