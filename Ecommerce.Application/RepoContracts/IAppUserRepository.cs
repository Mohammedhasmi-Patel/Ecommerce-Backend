using Ecommerce.Application.DTOs.Auth;
using Ecommerce.Domain.Entities;

namespace Ecommerce.Application.RepoContracts;

public interface IAppUserRepository
{
    public Task<AppUser> CreateAppUser(AppUser user,string password);
    public Task<AppUser?> GetAppUserByEmailAsync(string email);
    public Task<AppUser?> GetAppUserByIdAsync(string id);
    public Task<bool> EmailExistsAsync(string email);
    public Task<bool> VerifyPasswordAsync(AppUser user, string password);
    public Task<AppUser> UpdateAppUser(AppUser user,UpdateUserRequestDTO updateRequest);


}

