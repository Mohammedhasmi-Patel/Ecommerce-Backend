using System;
using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Application.DTOs.UserAddresses;

public class UserAddressRequestDTO
{
    [Required(ErrorMessage = "Address type is required (e.g. Home, Office)")]
    [MaxLength(50, ErrorMessage = "Address type cannot be more than 50 characters")]
    public string AddressType { get; set; } = null!;

    [Required(ErrorMessage = "Full name is required")]
    [MaxLength(100, ErrorMessage = "Full name cannot be more than 100 characters")]
    public string FullName { get; set; } = null!;

    [Required(ErrorMessage = "Phone number is required")]
    [MaxLength(20, ErrorMessage = "Phone number cannot be more than 20 characters")]
    [Phone(ErrorMessage = "Invalid phone number format")]
    public string PhoneNumber { get; set; } = null!;

    [Required(ErrorMessage = "Address Line 1 is required")]
    [MaxLength(200, ErrorMessage = "Address Line 1 cannot be more than 200 characters")]
    public string AddressLine1 { get; set; } = null!;

    [MaxLength(200, ErrorMessage = "Address Line 2 cannot be more than 200 characters")]
    public string AddressLine2 { get; set; } = string.Empty;

    [Required(ErrorMessage = "Country is required")]
    public Guid CountryId { get; set; }

    [Required(ErrorMessage = "State is required")]
    public Guid StateId { get; set; }

    [Required(ErrorMessage = "City is required")]
    public Guid CityId { get; set; }

    [Required(ErrorMessage = "Postal code is required")]
    [MaxLength(20, ErrorMessage = "Postal code cannot be more than 20 characters")]
    public string PostalCode { get; set; } = null!;

    public bool IsDefault { get; set; } = false;
}
