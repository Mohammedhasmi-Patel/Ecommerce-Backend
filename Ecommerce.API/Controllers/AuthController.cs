using Ecommerce.API.DTO.Auth;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.API.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : BaseController
{
    [HttpPost]
    [Route("register")]
    public IActionResult RegisterUserAsync([FromBody] RegisterUserRequestDTO registerRequest)
    {
        return Ok("Register success");
    }

}
