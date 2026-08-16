using Ecommerce.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.API.Controllers;

[ApiController]
[Route("api/cities")]
public class CityController : ControllerBase
{
    private readonly ICityService _cityService;

    public CityController(ICityService cityService)
    {
        _cityService = cityService;
    }

    [HttpGet]
    [Route("dropdown")]
    public async Task<IActionResult> Get([FromQuery] Guid? stateId, [FromQuery] string? search, CancellationToken cancellationToken)
    {
        var response = await _cityService.GetCitiesListDropdownAsync(stateId, search, cancellationToken);
        return Ok(response);
    }
}
