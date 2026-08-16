using Ecommerce.Application.DTOs;
using Ecommerce.Application.DTOs.Countries;
using Ecommerce.Application.Interfaces;
using Ecommerce.Application.Mappers;
using Ecommerce.Application.RepoContracts;


namespace Ecommerce.Application.Services;

public class CountryService : ICountryService
{
    private readonly ICountriesRepository _countriesRepository;

    public CountryService(ICountriesRepository countriesRepository)
    {
        _countriesRepository = countriesRepository;
    }

    public async Task<ApiResponse<List<CountiesListDropdownResponseDTO>>> GetCountriesListDropdownAsync(string? search, CancellationToken cancellationToken)
    {
        var countries = await _countriesRepository.GetAllAsync(search, cancellationToken);
        var countriesResponse = countries.Select(x => x.MapToCountiesListDropdownResponseDTO()).ToList();

        return ApiResponse<List<CountiesListDropdownResponseDTO>>.SuccessResponse(countriesResponse, "Countries list has been fetched successfully");

    }
}
