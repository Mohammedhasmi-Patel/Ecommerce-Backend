using Ecommerce.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.API.Controllers;

[ApiController]
[Route("api/countries")]
public class CountryController : ControllerBase
{
    private readonly ICountryService _countryService;
    public CountryController(ICountryService countryService)
    {
        _countryService = countryService;
    }
    [HttpGet]
    [Route("dropdown")]
    public async Task<IActionResult> Get([FromQuery] string? search, CancellationToken cancellationToken)
    {
        // return Ok();
        var response = await _countryService.GetCountriesListDropdownAsync(search, cancellationToken);
        return Ok(response);
    }
}
