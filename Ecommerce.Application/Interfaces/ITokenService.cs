using Ecommerce.Domain.Entities;

namespace Ecommerce.Application.Interfaces;

public interface ITokenService
{
    public Task<string> GenerateTokenAsync(AppUser user);
}

