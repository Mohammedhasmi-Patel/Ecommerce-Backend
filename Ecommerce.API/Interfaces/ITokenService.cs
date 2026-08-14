using Ecommerce.API.Entities;

namespace Ecommerce.API.Interfaces;

public interface ITokenService
{
    public Task<string> GenerateTokenAsync(AppUser user);
}
