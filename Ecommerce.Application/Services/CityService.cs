using Ecommerce.Application.DTOs;
using Ecommerce.Application.DTOs.Cities;
using Ecommerce.Application.Interfaces;
using Ecommerce.Application.Mappers;
using Ecommerce.Application.RepoContracts;

namespace Ecommerce.Application.Services;

public class CityService : ICityService
{
    private readonly ICitiesRepository _citiesRepository;

    public CityService(ICitiesRepository citiesRepository)
    {
        _citiesRepository = citiesRepository;
    }

    public async Task<ApiResponse<List<CityListDropdownResponseDTO>>> GetCitiesListDropdownAsync(Guid? stateId, string? search, CancellationToken cancellationToken)
    {
        var cities = await _citiesRepository.GetAllAsync(stateId, search, cancellationToken);
        var citiesResponse = cities.Select(x => x.MapToCityListDropdownResponseDTO()).ToList();

        return ApiResponse<List<CityListDropdownResponseDTO>>.SuccessResponse(citiesResponse, "Cities list has been fetched successfully");
    }
}
