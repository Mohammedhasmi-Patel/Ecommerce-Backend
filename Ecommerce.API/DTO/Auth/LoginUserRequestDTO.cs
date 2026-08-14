using System.ComponentModel.DataAnnotations;

namespace Ecommerce.API.DTO.Auth;

public class LoginUserRequestDTO
{
    [Required(ErrorMessage = "Email is required")]
    public string Email { get; set; } = null!;

    [Required(ErrorMessage = "Password is required")]
    public string Password { get; set; } = null!;

}
