using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Ecommerce.Application.DTOs.Auth;

public class RegisterUserRequestDTO
{
    // First Name
    [Required(ErrorMessage = "Firstname is required")]
    [MinLength(3, ErrorMessage = "Firstname cannot be less than 3 characters")]
    [MaxLength(50, ErrorMessage = "Firstname cannot be more than 50 characters")]
    public string FirstName { get; set; } = null!;

    // Last Name
    [Required(ErrorMessage = "Lastname is required")]
    [MinLength(3, ErrorMessage = "Lastname cannot be less than 3 characters")]
    [MaxLength(50, ErrorMessage = "Lastname cannot be more than 50 characters")]
    public string LastName { get; set; } = null!;

    // Avatar
    [Required(ErrorMessage = "Avatar is required")]
    public IFormFile Avatar { get; set; } = null!;

    // Email
    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid email address")]
    [MaxLength(100, ErrorMessage = "Email cannot be more than 100 characters")]
    public string Email { get; set; } = null!;

    // Password
    [Required(ErrorMessage = "Password is required")]
    [MinLength(8, ErrorMessage = "Password must be at least 8 characters long")]
    [MaxLength(100, ErrorMessage = "Password cannot be more than 100 characters")]
    [RegularExpression(
        @"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*[^a-zA-Z\d]).{8,}$",
        ErrorMessage = "Password must contain at least one uppercase letter, one lowercase letter, one digit, and one special character"
    )]
    public string Password { get; set; } = null!;

    // Confirm Password
    [Required(ErrorMessage = "Confirm password is required")]
    [Compare("Password", ErrorMessage = "Passwords do not match")]
    public string ConfirmPassword { get; set; } = null!;
}
