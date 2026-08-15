using Ecommerce.API.Entities;
using Ecommerce.API.Enum;
using Ecommerce.API.Exceptions;
using Ecommerce.API.RepoContracts;
using Microsoft.AspNetCore.Identity;

namespace Ecommerce.API.Repositories;

public class AppUserRepository : IAppUserRepository
{
    private readonly UserManager<AppUser> _usermanager;

    public AppUserRepository(UserManager<AppUser> userManager)
    {
        _usermanager = userManager;
    }
    public async Task<AppUser> CreateAppUser(AppUser user, string password)
    {
        var result = await _usermanager.CreateAsync(user, password);
        if (!result.Succeeded)
        {
            string errorMessage = result.Errors.FirstOrDefault()?.Description ?? "Something went wrong";
            throw new UnauthorizedException(errorMessage);
        }
        await _usermanager.AddToRoleAsync(user, nameof(UserRoleEnum.User));
        return user;
    }

    public async Task<bool> EmailExistsAsync(string email)
    {
        // throw new NotImplementedException();
        var user = await _usermanager.FindByEmailAsync(email);
        return user != null;
    }

    public async Task<AppUser?> GetAppUserByEmailAsync(string email)
    {
        return await _usermanager.FindByEmailAsync(email);
    }

    public async Task<AppUser?> GetAppUserByIdAsync(string id)
    {
        // throw new NotImplementedException();
        AppUser? user = await _usermanager.FindByIdAsync(id);
        return user;
    }

    public async Task<bool> VerifyPasswordAsync(AppUser user, string password)
    {
        var result = await _usermanager.CheckPasswordAsync(user, password);
        return result;
    }

}
