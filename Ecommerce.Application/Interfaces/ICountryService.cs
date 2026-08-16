using Ecommerce.Application.DTOs;
using Ecommerce.Application.DTOs.Countries;

namespace Ecommerce.Application.Interfaces;

public interface ICountryService
{
    public Task<ApiResponse<List<CountiesListDropdownResponseDTO>>> GetCountriesListDropdownAsync(string? search, CancellationToken cancellationToken);
}
