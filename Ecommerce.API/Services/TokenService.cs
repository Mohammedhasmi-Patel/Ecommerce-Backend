using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Ecommerce.API.Configurations;
using Ecommerce.API.Entities;
using Ecommerce.API.Enum;
using Ecommerce.API.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Ecommerce.API.Services;

public class TokenService : ITokenService
{
    private readonly UserManager<AppUser> _userManager;
    private readonly JwtConfiguration _jwtConfiguration;

    public TokenService(UserManager<AppUser> userManager, IOptions<JwtConfiguration> jwtConfiguration)
    {
        _userManager = userManager;
        _jwtConfiguration = jwtConfiguration.Value;
    }

    public async Task<string> GenerateTokenAsync(AppUser user)
    {
        List<Claim> claims = new List<Claim>()
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email ?? ""),
        };

        var userRole = (await _userManager.GetRolesAsync(user)).FirstOrDefault() ?? nameof(UserRoleEnum.User);
        claims.Add(new Claim(ClaimTypes.Role, userRole));

        var securityKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_jwtConfiguration.Secret));

        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _jwtConfiguration.Issuer,
            audience: _jwtConfiguration.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(_jwtConfiguration.JwtExpireMinutes),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
