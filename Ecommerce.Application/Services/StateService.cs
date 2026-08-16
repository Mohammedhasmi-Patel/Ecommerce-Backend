using Ecommerce.Application.DTOs;
using Ecommerce.Application.DTOs.States;
using Ecommerce.Application.Interfaces;
using Ecommerce.Application.Mappers;
using Ecommerce.Application.RepoContracts;

namespace Ecommerce.Application.Services;

public class StateService : IStateService
{
    private readonly IStatesRepository _statesRepository;

    public StateService(IStatesRepository statesRepository)
    {
        _statesRepository = statesRepository;
    }

    public async Task<ApiResponse<List<StateListDropdownResponseDTO>>> GetStatesListDropdownAsync(Guid? countryId, string? search, CancellationToken cancellationToken)
    {
        var states = await _statesRepository.GetAllAsync(countryId, search, cancellationToken);
        var statesResponse = states.Select(x => x.MapToStateListDropdownResponseDTO()).ToList();

        return ApiResponse<List<StateListDropdownResponseDTO>>.SuccessResponse(statesResponse, "States list has been fetched successfully");
    }
}
