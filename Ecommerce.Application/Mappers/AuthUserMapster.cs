using Ecommerce.Application.DTOs.Auth;
using Ecommerce.Domain.Entities;
using Ecommerce.Application.Common.Helpers;
namespace Ecommerce.Application.Mappers;

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

