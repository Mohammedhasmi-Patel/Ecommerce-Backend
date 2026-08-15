using Ecommerce.API.DTO.Auth;
using Ecommerce.API.Entities;
using Ecommerce.API.Helpers;
namespace Ecommerce.API.Mapster;

public static class AuthUserMapster
{

    public static AppUser ToAppUserFromRegister(this RegisterUserRequestDTO request, string avatarUrl)
    {
        return new AppUser()
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
            UserName = request.Email,
            Avatar = avatarUrl,
            IsActive = true,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = null,
            DeletedAt = null
        };
    }

    public static RegisterUserResponseDTO ToRegisterUserResponseDTO(this AppUser user, string token, string baseUrl)
    {
        return new RegisterUserResponseDTO()
        {
            FirstName = user.FirstName,
            LastName = user.LastName,
            Avatar = FileHelper.GetAvatarUrl(user.Avatar, baseUrl),
            Email = user.Email ?? string.Empty,
            Token = token
        };
    }


    public static LoginUserResponseDTO ToLoginUserResponseDTO(this AppUser user, string token, string baseUrl)
    {
        return new LoginUserResponseDTO()
        {
            FirstName = user.FirstName,
            LastName = user.LastName,
            Avatar = FileHelper.GetAvatarUrl(user.Avatar, baseUrl),
            Email = user.Email ?? string.Empty,
            Token = token
        };
    }

}
