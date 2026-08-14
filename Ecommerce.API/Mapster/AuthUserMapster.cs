using Ecommerce.API.DTO.Auth;
using Ecommerce.API.Entities;

namespace Ecommerce.API.Mapster;

public static class AuthUserMapster
{
    public static AppUser ToAppUserFromRegister(this RegisterUserRequestDTO request,string avatarUrl)
    {
        return new AppUser()
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            Avatar = avatarUrl,
            IsActive = true,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = null,
            DeletedAt = null
        };
    }

    public static RegisterUserResponseDTO ToRegisterUserResponseDTO(this AppUser user,string token)
    {
        return new RegisterUserResponseDTO()
        {
            FirstName = user.FirstName,
            LastName = user.LastName,
            Avatar = user.Avatar,
            Email = user.Email ?? string.Empty,
            Token = token
        };
    }

    
    public static LoginUserResponseDTO ToLoginUserResponseDTO(this AppUser user,string token)
    {
        return new LoginUserResponseDTO()
        {
            FirstName = user.FirstName,
            LastName = user.LastName,
            Avatar = user.Avatar,
            Email = user.Email ?? string.Empty,
            Token = token
        };
    }

}
