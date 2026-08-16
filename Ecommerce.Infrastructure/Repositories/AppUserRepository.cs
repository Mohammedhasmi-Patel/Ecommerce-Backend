using Ecommerce.Application.DTOs.Auth;
using Ecommerce.Application.RepoContracts;
using Ecommerce.Domain.Entities;
using Ecommerce.Domain.Enums;
using Ecommerce.Domain.Exceptions;
using Microsoft.AspNetCore.Identity;

namespace Ecommerce.Infrastructure.Repositories;

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

    public async Task<AppUser> UpdateAppUser(AppUser user, UpdateUserRequestDTO updateRequest)
    {
        user.FirstName = updateRequest.FirstName;
        user.LastName = updateRequest.LastName;
        
        if (user.Email != updateRequest.Email)
        {
            user.Email = updateRequest.Email;
            user.UserName = updateRequest.Email;
        }

        user.UpdatedAt = DateTime.UtcNow;

        var result = await _usermanager.UpdateAsync(user);
        if (!result.Succeeded)
        {
            string errorMessage = result.Errors.FirstOrDefault()?.Description ?? "Failed to update user";
            throw new BadRequestException(errorMessage);
        }

        return user;
    }



    public async Task<bool> VerifyPasswordAsync(AppUser user, string password)
    {
        var result = await _usermanager.CheckPasswordAsync(user, password);
        return result;
    }

}

