using Ecommerce.Application.DTOs.Auth;
using Ecommerce.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.API.Controllers;

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

    [HttpPost]
    [Route("login")]
    public async Task<IActionResult> LoginUserAsync([FromBody] LoginUserRequestDTO loginRequest,CancellationToken cancellationToken)
    {
        var response = await _authService.LoginUserAsync(loginRequest, cancellationToken);
        return Ok(response);
    }

    [HttpPut]
    [Route("update")]
    [Authorize]
    public async Task<IActionResult> UpdateUserAsync([FromForm] UpdateUserRequestDTO updateRequest, CancellationToken cancellationToken)
    {
        var response = await _authService.UpdateUserAsync(updateRequest, cancellationToken);
        return Ok(response);
    }

}


