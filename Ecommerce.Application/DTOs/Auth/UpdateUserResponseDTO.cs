namespace Ecommerce.Application.DTOs.Auth;

public class UpdateUserResponseDTO
{
    public string Id { get; set; } = null!; 
    public string FirstName { get; set; } = null!;   
    public string LastName { get; set; } = null!;   
    public string Email { get; set; } = null!;
    public string Avatar { get; set; } = null!;
}
