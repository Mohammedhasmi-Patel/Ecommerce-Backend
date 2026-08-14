using Ecommerce.API.DTO.Auth;
using Ecommerce.API.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.API.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : BaseController
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }
    [HttpPost]
    [Route("register")]
    public async Task<IActionResult> RegisterUserAsync([FromForm] RegisterUserRequestDTO registerRequest,CancellationToken cancellationToken)
    {
        var response = await _authService.RegisterUserAsync(registerRequest, cancellationToken);
        return Created(string.Empty, response);
    }

}
