using Ecommerce.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.API.Controllers;

[ApiController]
[Route("api/states")]
public class StateController : ControllerBase
{
    private readonly IStateService _stateService;

    public StateController(IStateService stateService)
    {
        _stateService = stateService;
    }

    [HttpGet]
    [Route("dropdown")]
    public async Task<IActionResult> Get([FromQuery] Guid? countryId, [FromQuery] string? search, CancellationToken cancellationToken)
    {
        var response = await _stateService.GetStatesListDropdownAsync(countryId, search, cancellationToken);
        return Ok(response);
    }
}
