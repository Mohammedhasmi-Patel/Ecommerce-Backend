namespace Ecommerce.API.DTO.Auth;

public class LoginUserResponseDTO
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string? Avatar { get; set; }
    public string Token { get; set; } = null!;
}
