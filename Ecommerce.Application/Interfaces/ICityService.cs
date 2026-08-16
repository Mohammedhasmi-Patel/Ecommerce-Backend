using Ecommerce.Application.DTOs;
using Ecommerce.Application.DTOs.Cities;

namespace Ecommerce.Application.Interfaces;

public interface ICityService
{
    Task<ApiResponse<List<CityListDropdownResponseDTO>>> GetCitiesListDropdownAsync(Guid? stateId, string? search, CancellationToken cancellationToken);
}
