using Ecommerce.API.Entities;

namespace Ecommerce.API.RepoContracts;

public interface IAppUserRepository
{
    public Task<AppUser> CreateAppUser(AppUser user,string password);
    public Task<AppUser?> GetAppUserByEmailAsync(string email);
    public Task<AppUser?> GetAppUserByIdAsync(string id);
    public Task<bool> EmailExistsAsync(string email);
    
}
