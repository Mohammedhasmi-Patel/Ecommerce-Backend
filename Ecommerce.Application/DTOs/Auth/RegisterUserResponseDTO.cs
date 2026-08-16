namespace Ecommerce.Application.DTOs.Auth;

public class RegisterUserResponseDTO
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string? Avatar { get; set; }
    public string Token {get;set;} = null!;
}

