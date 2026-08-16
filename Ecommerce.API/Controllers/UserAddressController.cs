
using System.Security.Claims;
using Ecommerce.Application.DTOs.UserAddresses;
using Ecommerce.Application.Interfaces;
using Ecommerce.Domain.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.API.Controllers;

[Authorize]
[Route("api/user-addresses")]
public class UserAddressController : BaseController
{
    private readonly IUserAddressService _userAddressService;

    public UserAddressController(IUserAddressService userAddressService)
    {
        _userAddressService = userAddressService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateAddress(
        [FromBody] UserAddressRequestDTO request, CancellationToken cancellationToken)
    {
        var userEmail = User.FindFirstValue(ClaimTypes.Email);
        if (string.IsNullOrEmpty(userEmail))
        {
            throw new UnauthorizedException("User not authenticated");
        }

        var response = await _userAddressService.CreateAddressAsync(userEmail, request, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }

    [HttpGet]
    public async Task<IActionResult> GetAddresses(CancellationToken cancellationToken)
    {
        var userEmail = User.FindFirstValue(ClaimTypes.Email);
        if (string.IsNullOrEmpty(userEmail))
        {
            throw new UnauthorizedException("User not authenticated");
        }

        var response = await _userAddressService.GetAddressesByUserAsync(userEmail, cancellationToken);
        return Ok(response);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetAddressById(
        Guid id,
        CancellationToken cancellationToken)
    {
        var userEmail = User.FindFirstValue(ClaimTypes.Email);
        if (string.IsNullOrEmpty(userEmail))
        {
            throw new UnauthorizedException("User not authenticated");
        }

        var response = await _userAddressService.GetAddressByIdAsync(userEmail, id, cancellationToken);
        return Ok(response);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateAddress(
       [FromRoute] Guid id,
        [FromBody] UserAddressRequestDTO request,
        CancellationToken cancellationToken)
    {
        var userEmail = User.FindFirstValue(ClaimTypes.Email);
        if (string.IsNullOrEmpty(userEmail))
        {
            throw new UnauthorizedException("User not authenticated");
        }

        var response = await _userAddressService.UpdateAddressAsync(userEmail, id, request, cancellationToken);
        return Ok(response);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteAddress(
        Guid id,
        CancellationToken cancellationToken)
    {
        var userEmail = User.FindFirstValue(ClaimTypes.Email);
        if (string.IsNullOrEmpty(userEmail))
        {
            throw new UnauthorizedException("User not authenticated");
        }

        var response = await _userAddressService.DeleteAddressAsync(userEmail, id, cancellationToken);
        return Ok(response);
    }
}
