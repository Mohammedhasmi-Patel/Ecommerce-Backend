using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Ecommerce.Application.DTOs.Auth;

public class UpdateUserRequestDTO
{
    [Required]
    [MaxLength(50)]
    [MinLength(2)]
    public string FirstName { get; set; } = null!;

    [Required]
    [MaxLength(50)]
    [MinLength(2)]
    public string LastName { get; set; } = null!;

    [Required]
    [MaxLength(100)]
    [EmailAddress]
    public string Email { get; set; } = null!;

    public string? OldAvatarPath { get; set; }

    public IFormFile? Avatar { get; set; }
}
